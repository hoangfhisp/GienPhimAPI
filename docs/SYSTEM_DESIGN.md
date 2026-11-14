# System Architecture and Design Document

## 1. Executive Architecture Summary

GienPhim API is structured as a **Domain-Driven Modular Monolith** implemented on ASP.NET Core (.NET 8.0). Rather than adopting a microservice fleet that introduces network serialization overhead and distributed transaction complexity at early phases, the platform organizes distinct business subdomains into isolated modules residing in a unified deployable unit.

Each module maintains strict layer decoupling:
- **Domain Layer**: Contains pure business entities, value objects, domain events, domain service contracts, and domain exceptions without dependencies on UI or database persistence frameworks.
- **Application Layer**: Houses business orchestrators, application services, data transfer objects (DTOs), validators, factory interfaces, and repository abstractions.
- **Infrastructure Layer**: Implements database persistence using Entity Framework Core DbContexts, external HTTP client adapters, cryptographic token utilities, and repository implementations.
- **API Layer**: Exposes HTTP REST controllers, middleware pipelines, and SignalR hubs.
- **Shared Kernel**: Supplies base exception definitions, generic `Result<T>` execution envelopes, and common validation markers.

```mermaid
graph TD
    subgraph Host ["ASP.NET Core Web Host (Program.cs)"]
        direction TB
        subgraph ModAuth ["AuthService Module"]
            AuthApi["Api (AuthController)"]
            AuthApp["Application (AuthOrchestrator)"]
            AuthDom["Domain (UserClaims, Tokens)"]
            AuthInfra["Infrastructure (AuthDbContext, JwtUtils)"]
            AuthApi --> AuthApp --> AuthDom
            AuthApp --> AuthInfra
        end

        subgraph ModFilm ["FilmService Module"]
            FilmApi["Api (PhimController, BotConfigController)"]
            FilmApp["Application (PhimOrchestrator, SyncPhimJob)"]
            FilmDom["Domain (Phim, TapPhim, TheLoai)"]
            FilmInfra["Infrastructure (FilmDbContext, ExternalApiClient)"]
            FilmApi --> FilmApp --> FilmDom
            FilmApp --> FilmInfra
        end

        subgraph ModInter ["InteractionService Module"]
            InterApi["Api (ChatGroupController, WatchPartyHub)"]
            InterApp["Application (ChatGroupOrchestrator)"]
            InterDom["Domain (ChatGroup, ChatMessage)"]
            InterInfra["Infrastructure (InteractionDbContext)"]
            InterApi --> InterApp --> InterDom
            InterApp --> InterInfra
        end

        subgraph ModWallet ["WalletService Module"]
            WalletApi["Api (WalletController)"]
            WalletApp["Application (WalletOrchestrator)"]
            WalletDom["Domain (Wallet, WalletTransaction)"]
            WalletInfra["Infrastructure (WalletDbContext, VnPayClient)"]
            WalletApi --> WalletApp --> WalletDom
            WalletApp --> WalletInfra
        end

        subgraph ModMember ["MembershipService Module"]
            MemberApi["Api (UserMembershipController)"]
            MemberApp["Application (UserMembershipOrchestrator)"]
            MemberDom["Domain (MembershipPlan, UserMembership)"]
            MemberInfra["Infrastructure (MembershipDbContext)"]
            MemberApi --> MemberApp --> MemberDom
            MemberApp --> MemberInfra
        end
    end
```

---

## 2. Bounded Context Decoupling & Inter-Service Communication

To guarantee that modules remain independently testable and maintainable, direct cross-domain entity referencing is strictly forbidden. Communication between bounded contexts adheres to two decoupled patterns:
1. **In-Process Dependency Injection (Shared Contracts)**: Shared interfaces registered with scoped lifetime across the runtime container.
2. **Typed HTTP Inter-Service Adapters**: When invoking operations across operational boundaries (such as `MembershipService` debiting a user's wallet via `WalletServiceClient`), requests transit through typed HTTP clients with explicit timeout and error isolation.

```mermaid
flowchart LR
    MembershipSub["Membership Purchase Request"] --> MemOrch["UserMembershipOrchestrator"]
    MemOrch -->|"Invoke via IWalletServiceClient"| WalletHttp["WalletServiceClient"]
    WalletHttp -->|"HTTP POST /api/Wallet/CreateWalletTransaction"| WalletCtrl["WalletController"]
    WalletCtrl --> WalletOrch["WalletOrchestrator"]
    WalletOrch --> WalletDb[(WalletDbContext)]
```

---

## 3. High-Value Sequence Execution Workflows

### 3.1 Automated Film Crawling and Delta Ingestion Workflow
The automated crawler pipeline periodically scans third-party media sources, evaluates delta changes, and updates local catalog records.

```mermaid
sequenceDiagram
    autonumber
    participant Quartz as Quartz Scheduler
    participant Job as SyncPhimJob
    participant Watcher as BotConfigWatcherJob
    participant ApiClient as IExternalApiClient
    participant PhimSvc as IPhimService
    participant UoW as IUnitOfWork (FilmDb)
    participant LogSvc as IBotSyncLogService

    Quartz->>Job: Trigger execution cycle
    Job->>UoW: Retrieve active BotConfig
    alt BotConfig.IsEnabled == false
        Job-->>Quartz: Halt execution (Bot disabled)
    else BotConfig.IsEnabled == true
        loop For Page = 1 to BotConfig.ApiPerRun
            Job->>ApiClient: GetLatestFilmsV3Async(page)
            ApiClient-->>Job: Page data (List of Film items)
            loop For Each Film in Items
                Job->>UoW: Phims.GetBySlugAsync(film.Slug)
                alt Film not found
                    Job->>PhimSvc: ImportPhimFromApiAsync(film.Slug)
                    PhimSvc->>UoW: Save new Phim & Episodes
                    Job->>LogSvc: CreateAsync(Action = Import)
                else Modified.Time > existing.ThoiGianCapNhat
                    Job->>PhimSvc: UpdatePhimFromApiAsync(film.Slug)
                    PhimSvc->>UoW: Update metadata & episodes
                    Job->>LogSvc: CreateAsync(Action = Update)
                else No updates detected
                    Job->>Job: Skip redundant ingestion
                end
            end
        end
    end
    Quartz->>Watcher: Execute watcher cycle (every 5 minutes)
    Watcher->>UoW: Check BotConfig.LastUpdated
    opt Configuration Modified
        Watcher->>Quartz: Reschedule SyncPhimJob with new IntervalHours
    end
```

---

### 3.2 VIP Membership Purchase and Balance Settlement Flow
Purchasing a VIP membership requires atomic coordination between membership validation and digital wallet debiting.

```mermaid
sequenceDiagram
    autonumber
    participant Client as Web/Mobile Client
    participant Auth as JWT Guard
    participant Controller as UserMembershipController
    participant Orchestrator as UserMembershipOrchestrator
    participant WalletClient as IWalletServiceClient
    participant Domain as UserMembershipDomainService
    participant MemberDb as MembershipDbContext
    participant WalletDb as WalletDbContext

    Client->>Controller: POST /api/UserMembership/Create (UserId, OptionId)
    Controller->>Auth: Verify JWT & Roles (User, Admin)
    Auth-->>Controller: Claims verified (UserId confirmed)
    Controller->>Orchestrator: CreateUserMembershipAsync(dto, price, duration)
    Orchestrator->>WalletClient: GetWalletByUserIdAsync(dto.UserId)
    WalletClient->>WalletDb: Query user wallet balance
    WalletDb-->>WalletClient: Return wallet (Balance: SoDu)
    WalletClient-->>Orchestrator: Wallet data returned
    alt SoDu < Price
        Orchestrator-->>Controller: Throw InvalidAmountException (Insufficient Balance)
        Controller-->>Client: HTTP 400 Bad Request
    else SoDu >= Price
        Orchestrator->>WalletClient: CreateWalletTransactionAsync(Debit SoTien, Description)
        WalletClient->>WalletDb: Deduct SoDu & Insert WalletTransaction
        WalletDb-->>WalletClient: Transaction committed
        Orchestrator->>Domain: EnsureCanCreate(userMembership)
        Domain-->>Orchestrator: Business validation passed
        Orchestrator->>MemberDb: Persist UserMembership (Activated & Expiration set)
        MemberDb-->>Orchestrator: Record saved
        Orchestrator-->>Controller: Return created entity & transaction ID
        Controller-->>Client: HTTP 200 OK (Membership Active)
    end
```

---

### 3.3 Real-Time Watch Party Playback Synchronization Flow
Watch party participants maintain synchronized video playback positions through WebSocket event broadcast.

```mermaid
sequenceDiagram
    autonumber
    participant Host as Room Host (Client A)
    participant SignalR as WatchPartyHub (/hubs/watch-party)
    participant Member as Room Member (Client B)

    Host->>SignalR: JoinRoom(groupId)
    Member->>SignalR: JoinRoom(groupId)
    SignalR-->>Host: Joined SignalR Group (groupId)
    SignalR-->>Member: Joined SignalR Group (groupId)

    Note over Host,Member: Playback Synchronization Event
    Host->>SignalR: SendPlayerEvent(groupId, "seek", currentTime = 450.5)
    SignalR->>Member: Broadcast ReceivePlayerEvent("seek", 450.5) to OthersInGroup
    Member->>Member: Reconcile video player timeline to 450.5s

    Note over Host,Member: Episode Transition Event
    Host->>SignalR: ChangeEpisode(groupId, EpisodeSyncDto)
    SignalR->>Member: Broadcast ReceivePlayerEvent("changeEpisode", EpisodeSyncDto)
    Member->>Member: Switch active stream URL and reset playback buffer
```

---

## 4. Multi-DbContext Partitioning and Data Strategy

To maintain clean separation between domains, data persistence is partitioned across 8 independent Entity Framework Core database contexts within Microsoft SQL Server:

| DbContext | Managed Domain Aggregate | Migration Scope |
| :--- | :--- | :--- |
| `AuthDbContext` | Refresh tokens, authentication audits | User session and token lifecycle tracking |
| `UserDbContext` | Users, Roles, Profile metadata | Core user identities, credentials, authorization |
| `FilmDbContext` | Films, Episodes, Directors, Actors, Genres, Tags, Views, History, BotConfigs, BotSyncLogs | Streaming media catalog and ingestion telemetry |
| `InteractionDbContext` | ChatGroups, ChatMessages, GroupMembers, JoinRequests, Feedbacks | Social collaboration and watch party room state |
| `MembershipDbContext` | MembershipPlans, PlanOptions, UserMemberships | VIP subscription tiers and entitlement validity |
| `WalletDbContext` | Wallets, WalletTransactions | Ledger entries, deposit audit, internal balance |
| `OrderDbContext` | Orders, OrderDetails | Customer cart checkout records |
| `PaymentDbContext` | PaymentMethods, PaymentTransactions | Low-level gateway payment records |

### Transaction Management
Within each individual context, mutations are encapsulated using the `UnitOfWork` and `EfTransaction` pattern. This guarantees that operations either commit completely or roll back in full, preserving database integrity.
