# API Specification and DTO Reference Catalog

## 1. Overview

GienPhim API delivers a comprehensive RESTful interface across all platform subdomains. All responses adhere to unified JSON envelope schemas providing consistent status codes, message descriptions, and data payloads.

---

## 2. Standard Response Envelopes

### 2.1 ResultDTO & ApiResponse Schema
Standard API operations return a consistent JSON payload:
```json
{
  "status": true,
  "msg": "Operation completed successfully",
  "data": { ... },
  "statusCode": 200
}
```

### 2.2 Paginated Response Schema (`ResultListDTO<T>`)
Paginated collections embed pagination metadata:
```json
{
  "status": true,
  "msg": "Data retrieved successfully",
  "data": [ ... ],
  "pagination": {
    "page": 1,
    "limit": 10,
    "total": 142,
    "totalPages": 15
  },
  "statusCode": 200
}
```

---

## 3. Authentication & Identity Endpoints (`/api/Auth`)

| Method | Endpoint | Authorization | Description |
| :--- | :--- | :--- | :--- |
| `POST` | `/api/Auth/Login` | Anonymous | Authenticates with email and password, returning JWT access token and refresh token. |
| `POST` | `/api/Auth/LoginWithGoogleToken` | Anonymous | Authenticates with a Google OAuth2 ID Token (`idToken`). |
| `POST` | `/api/Auth/RefreshToken` | Anonymous | Issues a new JWT access token using a valid unexpired refresh token. |
| `POST` | `/api/Auth/LogOut` | Authenticated | Revokes the active refresh token and invalidates the session. |
| `POST` | `/api/Auth/ForgotPassword` | Anonymous | Dispatches a 6-digit verification OTP to the user's email. |
| `POST` | `/api/Auth/VerifyOtpForgotPassword` | Anonymous | Verifies the OTP submitted for password recovery. |
| `POST` | `/api/Auth/ResetPassword` | Anonymous | Resets the user's password using the verified OTP token. |
| `POST` | `/api/Auth/SendOtpChangeEmail` | Authenticated | Sends an OTP to verify a new email address. |
| `POST` | `/api/Auth/VerifyChangeEmail` | Authenticated | Verifies OTP and updates user email. |

---

## 4. User & Role Management Endpoints (`/api/User`, `/api/Role`)

| Method | Endpoint | Authorization | Description |
| :--- | :--- | :--- | :--- |
| `POST` | `/api/User/Register` | Anonymous | Registers a new user account with initial standard role. |
| `HttpGet` | `/api/User/GetById/{id}` | Authenticated | Retrieves user profile metadata by user identifier. |
| `HttpGet` | `/api/User/GetByEmail/{email}` | Authenticated | Retrieves user information by registered email address. |
| `HttpGet` | `/api/User/GetAllUsers` | `Admin` | Retrieves paginated list of all registered users. |
| `HttpPut` | `/api/User/UpdateInfoUser` | Authenticated | Updates full name, phone number, and avatar image. |
| `HttpDelete` | `/api/User/DeleteUser` | `Admin` | Deactivates or removes a user record from the database. |
| `HttpGet` | `/api/Role/GetAllRoles` | `Admin` | Lists all defined system authorization roles. |
| `HttpPost` | `/api/Role/CreateRole` | `Admin` | Creates a new security role definition. |

---

## 5. Film Catalog & Metadata Endpoints (`/api/Phim`, `/api/*`)

### 5.1 Film Management (`/api/Phim`)
| Method | Endpoint | Authorization | Description |
| :--- | :--- | :--- | :--- |
| `HttpGet` | `/api/Phim/GetAllPhims` | Anonymous | Lists all published films with pagination filtering. |
| `HttpGet` | `/api/Phim/GetNewPhims` | Anonymous | Retrieves latest added films. |
| `HttpGet` | `/api/Phim/GetTopViewPhims` | Anonymous | Retrieves highest-viewed films sorted by view counts. |
| `HttpGet` | `/api/Phim/GetById/{id}` | Anonymous | Retrieves complete film profile including episodes, cast, and categories. |
| `HttpPost` | `/api/Phim/Create` | `Admin` | Creates a new film record (`multipart/form-data` with banner/poster). |
| `HttpPut` | `/api/Phim/Update` | `Admin` | Modifies existing film record. |
| `HttpDelete` | `/api/Phim/Delete` | `Admin` | Deletes a film and associated episode metadata. |
| `HttpPost` | `/api/Phim/ImportFromApi` | `Admin` | Manually triggers ingestion of a film by slug from PhimApi. |
| `HttpPut` | `/api/Phim/UpdateFromApi` | `Admin` | Manually updates an existing film with upstream modifications. |
| `HttpPost` | `/api/Phim/UpdatePhimByLuotLikeDisLike` | Anonymous | Increments or decrements film like/dislike counts. |

### 5.2 Episode & Taxonomy Endpoints
| Resource Endpoint | Methods Supported | Key Parameters |
| :--- | :--- | :--- |
| `/api/TapPhim` | `GET`, `POST`, `PUT`, `DELETE` | `phimId`, `soTap`, `tenTap`, `linkM3u8`, `linkEmbed` |
| `/api/TheLoai` | `GET`, `POST`, `PUT`, `DELETE` | `tenTheLoai`, `slug` |
| `/api/TheTag` | `GET`, `POST`, `PUT`, `DELETE` | `tenTag`, `slug` |
| `/api/DaoDien` | `GET`, `POST`, `PUT`, `DELETE` | `tenDaoDien`, `slug`, `hinhAnh` |
| `/api/DienVien` | `GET`, `POST`, `PUT`, `DELETE` | `tenDienVien`, `slug`, `hinhAnh` |
| `/api/QuocGia` | `GET`, `POST`, `PUT`, `DELETE` | `tenQuocGia`, `slug` |
| `/api/NgonNgu` | `GET`, `POST`, `PUT`, `DELETE` | `tenNgonNgu`, `slug` |
| `/api/FilmView` | `GET`, `POST` | `phimId`, increments view metrics |
| `/api/LichSuXem` | `GET`, `POST`, `DELETE` | `userId`, `phimId`, `tapPhimId`, `thoiGianXem` |

### 5.3 Automated Ingestion Bot Controls
| Method | Endpoint | Authorization | Description |
| :--- | :--- | :--- | :--- |
| `/api/BotConfig` | `GET`, `PUT` | `Admin` | Inspects and modifies crawler settings (`IsEnabled`, `IntervalHours`, `ApiPerRun`). |
| `/api/BotSyncLogs` | `GET` | `Admin` | Queries historical sync logs filtered by date and action. |

---

## 6. Real-Time Interaction & Watch Party Endpoints (`/api/ChatGroup`, `/api/*`)

| Method | Endpoint | Authorization | Description |
| :--- | :--- | :--- | :--- |
| `HttpGet` | `/api/ChatGroup/Detail/{groupId}` | Anonymous | Retrieves public room details, film association, and participant limits. |
| `HttpGet` | `/api/ChatGroup/ByFilm/{phimId}` | Anonymous | Lists active public watch party rooms currently streaming a specific film. |
| `HttpGet` | `/api/ChatGroup/Mine` | Authenticated | Lists rooms owned by or joined by the authenticated user. |
| `HttpPost` | `/api/ChatGroup/Create` | Authenticated | Creates a new watch party room linked to a selected film and episode. |
| `HttpGet` | `/api/ChatMessage/GetByGroup/{groupId}` | Authenticated | Retrieves paginated historical chat messages within a room. |
| `HttpGet` | `/api/GroupMember/GetByGroup/{groupId}` | Authenticated | Retrieves current members and roles within a group. |
| `HttpPost` | `/api/JoinRequest/Create` | Authenticated | Submits a request to join a private watch party room. |
| `HttpPost` | `/api/FeedBack/Create` | Authenticated | Submits user review or feedback rating for a film. |

---

## 7. Membership & Subscription Endpoints (`/api/MembershipPlan`, `/api/*`)

| Method | Endpoint | Authorization | Description |
| :--- | :--- | :--- | :--- |
| `HttpGet` | `/api/MembershipPlan/GetAll` | Anonymous | Lists all available subscription plans (e.g., Premium, VIP Cinema). |
| `HttpGet` | `/api/PlanOption/GetByPlanId` | Anonymous | Retrieves duration and pricing options (e.g., 30 days, 90 days, 365 days). |
| `HttpGet` | `/api/UserMembership/CurrentMembership` | Authenticated | Retrieves active subscription details and remaining days for a user. |
| `HttpPost` | `/api/UserMembership/Create` | Authenticated | Purchases a membership option, debiting the user's wallet automatically. |
| `HttpGet` | `/api/UserMembership/CheckUserMembership` | Authenticated | Validates whether a user holds active entitlement for a specific tier. |

---

## 8. Digital Wallet & VNPay Endpoints (`/api/Wallet`)

| Method | Endpoint | Authorization | Description |
| :--- | :--- | :--- | :--- |
| `HttpGet` | `/api/Wallet/GetWalletByUserId` | Authenticated | Returns current balance and wallet status for the user. |
| `HttpPost` | `/api/Wallet/CreateWallet` | Authenticated | Automatically provisions a digital wallet account upon registration. |
| `HttpGet` | `/api/Wallet/GetWalletTransactionByWalletId` | Authenticated | Retrieves paginated transaction history (deposits, subscription debits). |
| `HttpPost` | `/api/Wallet/CreateVnPay` | Authenticated | Generates a signed VNPay payment URL for a specified deposit amount. |
| `HttpGet` | `/api/Wallet/CallbackVnPay` | Anonymous | Inbound webhook callback from VNPay; validates HMAC-SHA512 checksum, credits balance, and redirects to frontend. |
