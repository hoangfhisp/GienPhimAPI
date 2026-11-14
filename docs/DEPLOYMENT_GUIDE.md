# Production Deployment and Operations Guide

## 1. System Requirements and Prerequisites

| Component | Minimum Specification | Recommended Production Specification |
| :--- | :--- | :--- |
| **Operating System** | Ubuntu 22.04 LTS / Windows Server 2022 | Ubuntu 24.04 LTS / Debian 12 |
| **CPU Architecture** | x86_64 (2 Cores) | x86_64 (4+ Cores) |
| **System Memory (RAM)** | 4 GB | 8 GB - 16 GB |
| **Storage Capacity** | 40 GB SSD | 200+ GB NVMe SSD (Media Storage) |
| **Runtime Environment** | .NET 8.0 ASP.NET Core Hosting Bundle | .NET 8.0 Runtime Container |
| **Database Server** | Microsoft SQL Server 2019 Standard | Microsoft SQL Server 2022 Enterprise |
| **Reverse Proxy** | Nginx 1.22+ or IIS 10.0+ | Nginx 1.24+ with HTTP/2 and WebSocket support |

---

## 2. Release Compilation and Artifact Packaging

Compile the solution into an optimized release package:
```bash
dotnet publish GienPhim_Services/GienPhim_Services.csproj \
  -c Release \
  -o ./dist \
  --no-self-contained \
  -p:PublishReadyToRun=true
```

---

## 3. Database Schema Provisioning and Migration

Before starting the application, apply all Entity Framework Core migrations across the 8 database contexts:

```bash
export ASPNETCORE_ENVIRONMENT=Production
cd GienPhim_Services

# Execute migrations sequentially for each isolated context
dotnet ef database update --context AuthDbContext
dotnet ef database update --context UserDbContext
dotnet ef database update --context FilmDbContext
dotnet ef database update --context InteractionDbContext
dotnet ef database update --context MembershipDbContext
dotnet ef database update --context WalletDbContext
dotnet ef database update --context OrderDbContext
dotnet ef database update --context PaymentDbContext
```

---

## 4. Containerized Deployment (Docker & Compose)

### 4.1 Production Multi-Stage Dockerfile
Create a `Dockerfile` at the repository root:

```dockerfile
# Stage 1: Build & Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy solution and project definitions
COPY *.sln .
COPY GienPhim_Services/*.csproj ./GienPhim_Services/
COPY Shared.FileStorage/*.csproj ./Shared.FileStorage/

# Restore dependencies
RUN dotnet restore

# Copy entire source tree and build release
COPY . .
RUN dotnet publish GienPhim_Services/GienPhim_Services.csproj -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime Environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Expose standard HTTP/HTTPS ports
EXPOSE 8080
EXPOSE 8443

# Environment settings
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Copy published binaries from build stage
COPY --from=build-env /app/publish .

# Persist wwwroot upload directories
VOLUME ["/app/wwwroot"]

ENTRYPOINT ["dotnet", "GienPhim_Services.dll"]
```

### 4.2 Production Compose File (`docker-compose.yml`)
```yaml
version: '3.8'

services:
  database:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: gienphim-mssql
    restart: always
    environment:
      - ACCEPT_EULA=Y
      - MSSQL_SA_PASSWORD=YourStrongPassword123!
    ports:
      - "1433:1433"
    volumes:
      - mssql_data:/var/opt/mssql

  api:
    build:
      context: .
      dockerfile: Dockerfile
    container_name: gienphim-backend
    restart: always
    depends_on:
      - database
    ports:
      - "5000:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__AuthDb=Server=database,1433;Database=GienPhim_Auth;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True;
      - ConnectionStrings__UserDb=Server=database,1433;Database=GienPhim_User;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True;
      - ConnectionStrings__FilmDb=Server=database,1433;Database=GienPhim_Film;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True;
      - ConnectionStrings__InteractionDb=Server=database,1433;Database=GienPhim_Interaction;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True;
      - ConnectionStrings__MembershipDb=Server=database,1433;Database=GienPhim_Membership;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True;
      - ConnectionStrings__PaymentDb=Server=database,1433;Database=GienPhim_Payment;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True;
      - ConnectionStrings__WalletDb=Server=database,1433;Database=GienPhim_Wallet;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True;
      - ConnectionStrings__OrderDb=Server=database,1433;Database=GienPhim_Order;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True;
      - AuthOptions__Jwt__SecretKey=YourProductionSecretKeyAtLeast32CharsLong!
    volumes:
      - media_storage:/app/wwwroot

volumes:
  mssql_data:
  media_storage:
```

---

## 5. Nginx Reverse Proxy Configuration

Nginx acts as the edge reverse proxy, managing SSL/TLS termination and forwarding WebSocket connections to SignalR:

```nginx
map $http_upgrade $connection_upgrade {
    default upgrade;
    ''      close;
}

server {
    listen 80;
    server_name api.gienphim.vn;
    return 301 https://$host$request_uri;
}

server {
    listen 443 ssl http2;
    server_name api.gienphim.vn;

    ssl_certificate /etc/letsencrypt/live/api.gienphim.vn/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/api.gienphim.vn/privkey.pem;
    ssl_protocols TLSv1.2 TLSv1.3;
    ssl_ciphers HIGH:!aNULL:!MD5;

    # Maximum file upload size for banners and videos
    client_max_body_size 500M;

    # General REST API routing
    location / {
        proxy_pass http://127.0.0.1:5000;
        proxy_http_version 1.1;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }

    # SignalR WebSocket routing (WatchPartyHub & ChatGroupHub)
    location /hubs/ {
        proxy_pass http://127.0.0.1:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection $connection_upgrade;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_read_timeout 300s;
        proxy_send_timeout 300s;
    }

    # Static assets serving
    location /images/ {
        alias /var/www/gienphim/wwwroot/images/;
        expires 30d;
        add_header Cache-Control "public, no-transform";
    }
}
```

---

## 6. Linux Systemd Service Definition

When deploying directly to a Linux host without Docker:
Create `/etc/systemd/system/gienphim.service`:

```ini
[Unit]
Description=GienPhim ASP.NET Core Web API Platform
After=network.target

[Service]
WorkingDirectory=/var/www/gienphim
ExecStart=/usr/bin/dotnet /var/www/gienphim/GienPhim_Services.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=gienphim-api
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

Enable and start the service:
```bash
sudo systemctl daemon-reload
sudo systemctl enable gienphim.service
sudo systemctl start gienphim.service
sudo systemctl status gienphim.service
```

---

## 7. Operations, Monitoring & Maintenance

1. **Quartz Scheduler Supervision**: Monitor `BotSyncLogs` in the database to verify that `SyncPhimJob` executes within scheduled intervals and error rates remain at 0%.
2. **Storage Pruning**: Monitor disk usage in `wwwroot/videos` and `wwwroot/images`. Set up automated retention policies or configure an off-box object storage provider (e.g., S3-compatible storage) when volume exceeds 500GB.
3. **Database Backups**: Perform automated daily full backups and hourly differential transaction log backups of all 8 production databases.
