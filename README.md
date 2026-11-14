<div align="center">

# GienPhim Modular Monolith API Platform

### Enterprise High-Throughput Media Streaming, Watch Party Synchronization, and Payment Settlement Engine

[![Runtime](https://img.shields.io/badge/Runtime-.NET%208.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/Language-C%23%2012-239120?style=flat-square&logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Framework](https://img.shields.io/badge/Framework-ASP.NET%20Core-512BD4?style=flat-square&logo=dotnet)](https://learn.microsoft.com/aspnet/core)
[![Realtime](https://img.shields.io/badge/Realtime-SignalR-F44336?style=flat-square&logo=signalr)](https://learn.microsoft.com/aspnet/core/signalr)
[![Database](https://img.shields.io/badge/Database-SQL%20Server-CC292B?style=flat-square&logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![Background Jobs](https://img.shields.io/badge/Scheduler-Quartz.NET-00599C?style=flat-square)](https://www.quartz-scheduler.net/)
[![Payment Gateway](https://img.shields.io/badge/Gateway-VNPay-0066B2?style=flat-square)](https://vnpay.vn/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20%2F%20Modular%20Monolith-blue?style=flat-square)](#application-architecture)
[![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)](LICENSE)

<p align="center">
  <a href="#project-overview">Overview</a> &bull;
  <a href="#core-capabilities">Capabilities</a> &bull;
  <a href="#application-architecture">Architecture</a> &bull;
  <a href="#role-based-feature-matrix">Role Matrix</a> &bull;
  <a href="#system-highlights">System Highlights</a> &bull;
  <a href="#quickstart">Quickstart</a> &bull;
  <a href="#documentation-hub">Documentation Hub</a> &bull;
  <a href="#deployment--build">Deployment</a> &bull;
  <a href="#license">License</a>
</p>

</div>

---

> [!IMPORTANT]
> **Production Runtime & Environmental Notice**
> 1. This system requires .NET 8.0 SDK / Runtime and Microsoft SQL Server 2019 or newer.
> 2. All 8 modular database contexts require valid connection strings defined in `appsettings.json` or system environment variables before initial boot.
> 3. Outbound synchronization workers require outbound TLS 1.2 / TLS 1.3 network transport enabled at the host operating system level to interface with external media providers.
> 4. SignalR streaming endpoints enforce strict CORS validation; client origins must be registered under `AllowedOrigins` with credentials transmission permitted.

---

## Project Overview

GienPhim API is an enterprise-grade backend infrastructure architected to power digital cinema platforms, automated content ingestion pipelines, and interactive social streaming environments. Traditional video-on-demand services face significant friction in three major domains:
1. Operational overhead involved in manual media ingestion and catalog synchronization from content aggregators.
2. Latency and state drift when orchestrating synchronized social watch parties across geographically distributed viewers.
3. Complex monetary reconciliation between external payment gateways, user digital wallets, and tiered recurring subscription memberships.

GienPhim addresses these challenges by implementing a domain-driven Modular Monolith architecture within ASP.NET Core. The platform unites decoupled bounded contexts—Identity, User Profile, Film Catalog, Watch Party Interaction, Membership Subscription, and Digital Wallet Ledger—into an unified runtime while preserving strict domain boundaries, automated Quartz.NET synchronization jobs, bidirectional SignalR communication channels, and cryptographically verified VNPay payment integrations.

---

## Core Capabilities

### 1. Automated Media Ingestion and Delta Synchronization
- **Continuous Background Crawling**: Implements a dedicated Quartz.NET scheduling engine (`SyncPhimJob`) that regularly queries external media APIs (`https://phimapi.com/`).
- **Dynamic Watcher Scheduling**: Leverages `BotConfigWatcherJob` to monitor database configuration updates (`BotConfig`) in real time, recalculating intervals and firing triggers without requiring service restarts.
- **Incremental Deduplication**: Identifies new film assets and verifies modification timestamps (`Modified.Time > ThoiGianCapNhat`) to only ingest delta updates, preventing redundant database I/O.
- **Audit Trails**: Records full sync history, import statuses, and operational telemetry into `BotSyncLog` records for automated diagnostic observability.

### 2. Real-Time Watch Party and Social Interaction
- **Synchronized Playback Engine**: Provides low-latency media playback coordination via SignalR (`WatchPartyHub`), broadcasting play, pause, seek, and episode transition events across all joined participants.
- **Group Governance**: Supports room creation, membership access control, invitation approval workflows, and role assignments (Host, Member).
- **In-Room Streaming Chat**: High-performance transient messaging infrastructure coupled with persistent room feedback tracking and moderation hooks.

### 3. Digital Wallet Ledger and Financial Settlement
- **Double-Entry Financial Tracking**: Maintains user wallet balances with immutable audit logs (`WalletTransaction`) recording credits, debits, source references, and transaction timestamps.
- **VNPay Gateway Integration**: Generates compliant VNPay checkout URLs with HMAC-SHA512 checksum signatures, processes IPN callbacks, validates return parameters, and credits balances atomically.
- **Subscription Orchestration**: Facilitates automated settlement for tiered VIP membership options through internal wallet debiting with balance validation before plan activation.

### 4. Tiered VIP Membership Management
- **Configurable Subscription Plans**: Exposes granular plan definitions (`MembershipPlan`) paired with duration and pricing options (`PlanOption`).
- **Automated Lifecycle Enforcement**: Evaluates active subscriptions, calculates expiration dates, and controls feature access (ad-free viewing, VIP catalog access, premium room creation).

### 5. Identity, Access Control, and Security Perimeter
- **Dual Authentication Vectors**: Standard email/password credentials paired with Google OAuth2 OpenID Connect verification (`Google.Apis.Auth`).
- **Cryptographic Token Lifecycle**: Stateless JWT issuance (`System.IdentityModel.Tokens.Jwt`) reinforced by database-tracked Refresh Tokens, supporting token rotation and explicit revocation.
- **Role-Based Authorization**: Granular RBAC enforcement through declarative attributes (`[Authorize(Roles = "Admin")]`, `[Authorize(Roles = "User,Admin")]`).

---

## Role-Based Feature Matrix

| Functional Scope | Anonymous Guest | Registered User | Active VIP Member | System Administrator |
| :--- | :--- | :--- | :--- | :--- |
| Public Film Discovery & Searching | Supported | Supported | Supported | Supported |
| Standard Resolution Streaming | Supported | Supported | Supported | Supported |
| VIP Exclusive Content Access | Restricted | Restricted | Supported | Supported |
| Watch Party Participation (Join) | Restricted | Supported | Supported | Supported |
| Watch Party Room Hosting | Restricted | Restricted | Supported | Supported |
| Digital Wallet Deposit (VNPay) | Not Applicable | Supported | Supported | Supported |
| VIP Plan Subscription Purchase | Not Applicable | Supported | Supported | Supported |
| Viewing History & Interaction Feedback | Restricted | Supported | Supported | Supported |
| Film Catalog Management (CRUD) | Restricted | Restricted | Restricted | Supported |
| Media Asset Upload (CDN/Storage) | Restricted | Restricted | Restricted | Supported |
| Automated Crawler Configuration | Restricted | Restricted | Restricted | Supported |
| User & Role Administration | Restricted | Restricted | Restricted | Supported |
| Financial Ledger Auditing | Restricted | Restricted | Restricted | Supported |

---

## System Highlights

| Capability | Module / Service | Technical Description |
| :--- | :--- | :--- |
| Identity & Authentication | `AuthService` | JWT generation, Google OAuth2 ID token verification, refresh token persistence, OTP verification via MailKit SMTP. |
| User Account Management | `UserService` | User profile storage, role assignment, avatar management, and internal identity projection. |
| Catalog & Metadata Engine | `FilmService` | Film entity management, episode indexing, director/actor taxonomy, categorization, tagging, and view counters. |
| Background Ingestion Worker | `FilmService.Application.Jobs` | Quartz.NET scheduled jobs (`SyncPhimJob`, `BotConfigWatcherJob`) for external catalog consumption and resilient updates. |
| Real-time Synchronization | `InteractionService` / `Hubs` | ASP.NET Core SignalR hubs (`WatchPartyHub`, `ChatGroupHub`) synchronizing video playback state, room settings, and chat. |
| Social Interaction Engine | `InteractionService` | Group formation, member governance, join requests, and user feedback mechanisms. |
| VIP Membership Monetization | `MembershipService` | Subscription tier definitions, plan options, expiration enforcement, and wallet-coordinated purchases. |
| Financial Ledger & Gateway | `WalletService` | Balance accounting, transaction ledgering, and VNPay merchant payment gateway HMAC-SHA512 checksum validation. |
| Asset Storage Service | `Shared.FileStorage` | Disk-based media asset pipeline providing safe naming sanitization (`SlugHelper`) and asset streaming (`FileHelper`). |

---

## Application Architecture

```mermaid
graph TD
    subgraph Client_Layer ["Client Presentation Layer"]
        SPA["Web Application (React / Next.js)"]
        Mobile["Mobile Application (Flutter / Native)"]
    end

    subgraph Entry_Perimeter ["API Gateway & Host Perimeter"]
        Kestrel["Kestrel HTTP Server"]
        CorsMiddleware["CORS & Static Files Middleware"]
        AuthMiddleware["JWT Authentication Middleware"]
        ValidationMiddleware["Model Validation Filter"]
        RoutingEngine["Routing & Exception Handlers"]
    end

    subgraph Realtime_Layer ["Real-Time Transport Layer"]
        WatchHub["WatchPartyHub (/hubs/watch-party)"]
        ChatHub["ChatGroupHub (/hubs/chat-group)"]
    end

    subgraph Modular_Services ["Modular Monolith Domain Services"]
        AuthSvc["AuthService<br/>Identity & Tokens"]
        UserSvc["UserService<br/>User & Roles"]
        FilmSvc["FilmService<br/>Catalog & Metadata"]
        InterSvc["InteractionService<br/>Watch Party & Social"]
        MemberSvc["MembershipService<br/>Plans & Subscriptions"]
        WalletSvc["WalletService<br/>Ledger & Transactions"]
    end

    subgraph Background_Workers ["Automated Quartz.NET Workers"]
        SyncJob["SyncPhimJob<br/>(Catalog Ingestion)"]
        WatcherJob["BotConfigWatcherJob<br/>(Dynamic Trigger Reload)"]
    end

    subgraph Persistence_Layer ["Microsoft SQL Server Multi-Context Partitioning"]
        AuthDb[(AuthDb)]
        UserDb[(UserDb)]
        FilmDb[(FilmDb)]
        InterDb[(InteractionDb)]
        MemberDb[(MembershipDb)]
        WalletDb[(WalletDb)]
        OrderDb[(OrderDb)]
        PaymentDb[(PaymentDb)]
    end

    subgraph External_Integrations ["External Third-Party Ecosystem"]
        PhimApi["PhimApi Content Provider<br/>(phimapi.com)"]
        VNPay["VNPay Merchant Gateway"]
        GoogleOIDC["Google Identity Platform"]
        SMTPGateway["SMTP Mail Server (MailKit)"]
    end

    SPA -->|HTTPS / REST| Entry_Perimeter
    Mobile -->|HTTPS / REST| Entry_Perimeter
    SPA -->|WSS / SignalR| Realtime_Layer
    Mobile -->|WSS / SignalR| Realtime_Layer

    Entry_Perimeter --> AuthSvc
    Entry_Perimeter --> UserSvc
    Entry_Perimeter --> FilmSvc
    Entry_Perimeter --> InterSvc
    Entry_Perimeter --> MemberSvc
    Entry_Perimeter --> WalletSvc

    Realtime_Layer --> InterSvc

    Background_Workers -->|Reads Config & Ingests| FilmSvc

    AuthSvc --> AuthDb
    UserSvc --> UserDb
    FilmSvc --> FilmDb
    InterSvc --> InterDb
    MemberSvc --> MemberDb
    WalletSvc --> WalletDb

    MemberSvc -.->|Internal HTTP / Client| WalletSvc
    AuthSvc -.->|Internal HTTP / Client| UserSvc
    InterSvc -.->|Internal HTTP / Client| FilmSvc

    SyncJob -->|Outbound REST (TLS 1.2/1.3)| PhimApi
    WalletSvc -->|HMAC-SHA512 Checksum| VNPay
    AuthSvc -->|OIDC Token Validation| GoogleOIDC
    AuthSvc -->|MailKit Dispatch| SMTPGateway
```

---

## Quickstart

### Step 1: Clone Repository and Configure Environment
Clone the repository and prepare the environment configuration file:
```bash
git clone https://github.com/hoangfhisp/GienPhimAPI.git
cd GienPhimAPI
copy .env.example GienPhim_Services\appsettings.Development.json
```
Populate `appsettings.Development.json` with target SQL Server connection strings and service secrets:
```json
{
  "ConnectionStrings": {
    "AuthDb": "Server=localhost,1433;Database=GienPhim_Auth;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;",
    "UserDb": "Server=localhost,1433;Database=GienPhim_User;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;",
    "FilmDb": "Server=localhost,1433;Database=GienPhim_Film;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;",
    "InteractionDb": "Server=localhost,1433;Database=GienPhim_Interaction;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;",
    "MembershipDb": "Server=localhost,1433;Database=GienPhim_Membership;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;",
    "PaymentDb": "Server=localhost,1433;Database=GienPhim_Payment;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;",
    "WalletDb": "Server=localhost,1433;Database=GienPhim_Wallet;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;",
    "OrderDb": "Server=localhost,1433;Database=GienPhim_Order;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;"
  },
  "AuthOptions": {
    "Jwt": {
      "SecretKey": "YourSecureKeyOfAtLeast32CharactersLength",
      "Issuer": "GienPhimIdentity",
      "Audience": "GienPhimClient",
      "ExpireMinutes": 30
    },
    "RefreshTokenExpireDays": 15
  },
  "ServiceUrls": {
    "UserService": "https://localhost:5001",
    "FilmService": "https://localhost:5001",
    "PhimApi": "https://phimapi.com/",
    "WalletService": "https://localhost:5001"
  }
}
```

### Step 2: Restore Dependencies and Apply Database Migrations
Restore all NuGet dependencies across the solution:
```bash
dotnet restore
```
Apply Entity Framework Core migrations to provision all context schemas:
```bash
cd GienPhim_Services
dotnet ef database update --context AuthDbContext
dotnet ef database update --context UserDbContext
dotnet ef database update --context FilmDbContext
dotnet ef database update --context InteractionDbContext
dotnet ef database update --context MembershipDbContext
dotnet ef database update --context WalletDbContext
dotnet ef database update --context OrderDbContext
dotnet ef database update --context PaymentDbContext
cd ..
```

### Step 3: Run the Application
Launch the backend service using the .NET CLI:
```bash
dotnet run --project GienPhim_Services/GienPhim_Services.csproj --launch-profile https
```
The application will bind to the configured ports:
- Swagger Interactive Documentation: `https://localhost:5001/swagger`
- Real-Time Watch Party Hub: `wss://localhost:5001/hubs/watch-party`
- Group Settings Hub: `wss://localhost:5001/hubs/chat-group`

---

## Documentation Hub

Comprehensive, in-depth architectural and operational guides are organized within the `docs/` repository:

| Document | Primary Focus | Target Audience |
| :--- | :--- | :--- |
| [System Architecture & Design](docs/SYSTEM_DESIGN.md) | Modular Monolith patterns, DDD layer decoupling, and sequence execution diagrams. | System Architects, Tech Leads |
| [Real-Time Engine & Data Pipelines](docs/REALTIME_OR_DATA_PIPELINE.md) | SignalR Watch Party synchronization, drift correction, and Quartz.NET crawling pipeline. | Backend & Real-Time Engineers |
| [Integration & Native Storage Engine](docs/NATIVE_OR_INTEGRATION_ENGINE.md) | External API integrations (PhimApi, VNPay, Google OAuth2, SMTP) and local asset storage. | Integration & DevOps Engineers |
| [API Specifications & DTO Catalog](docs/API_SPECIFICATION.md) | RESTful API endpoint index, parameters, DTO payloads, and standard response envelopes. | Frontend & API Consumers |
| [State, Identity & Security Governance](docs/STATE_AND_SECURITY.md) | JWT token rotation, RBAC permission matrix, VNPay cryptographic hashing, and TLS standards. | Security & Compliance Engineers |
| [Production Deployment Guide](docs/DEPLOYMENT_GUIDE.md) | Containerization (Docker), Nginx reverse proxy configuration, SSL setup, and maintenance. | DevOps & Site Reliability Engineers |

---

## Deployment & Build

The application compiles into cross-platform binaries deployable across bare metal, virtual machines, or container engines.

### 1. Release Binary Compilation
```bash
dotnet publish GienPhim_Services/GienPhim_Services.csproj -c Release -o ./publish
```

### 2. Containerized Deployment (Docker)
Build and run the platform using container specifications:
```bash
docker build -t gienphim-api:latest -f Dockerfile .
docker run -d -p 5000:8080 --name gienphim-backend gienphim-api:latest
```

For complete instructions regarding production systemd units, IIS reverse proxies, and Nginx WebSocket forwarding configurations, consult the [Production Deployment Guide](docs/DEPLOYMENT_GUIDE.md).

---

## License

This project is distributed under the terms of the **MIT License**. See the [LICENSE](LICENSE) file for complete terms and permissions.
