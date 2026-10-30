# Feature: Auth0-Protected Test Stock API

## Purpose

Provide a separate test API for exercising stock CRUD requests. The API stores sample records in memory and does not connect to FinMind or a database.

## Structure and Routes

The API project is `Stock.Api`, at the repository root beside `StockChips.Web`.

- `GET /api/stocks` — list stocks.
- `GET /api/stocks/{id}` — retrieve one stock.
- `POST /api/stocks` — create a stock using `symbol` and `name`.
- `PUT /api/stocks/{id}` — replace a stock's `symbol` and `name`.
- `DELETE /api/stocks/{id}` — delete a stock.

Every stock route requires an Auth0 access token sent as `Authorization: Bearer <token>`. Records are temporary and reset when the process restarts. These records are test fixtures, not market data or financial advice.

## Auth0 Configuration

- Tenant domain: `dev-w7qt8cqjv3ku3d2l.us.auth0.com` (inferred from the Auth0 dashboard URL supplied for this task).
- Suggested API identifier/audience: `https://stock-api` (create this identifier in the Auth0 dashboard exactly as written).
- Signing algorithm: `RS256`.
- Runtime settings: `Auth0__Domain` and `Auth0__Audience`, or matching values in `appsettings.json`.

### Dashboard Setup

1. Open **Applications → APIs → Create API**.
2. Set the name to `Stock API`, the identifier to `https://stock-api`, and the signing algorithm to `RS256`.
3. Create the API. The identifier cannot be changed later, so make sure it exactly matches the configured audience.
4. In the new API's **Test** tab, authorize or create a Machine-to-Machine test application for this API and request a token. The test token for the Auth0 Management API is for a different audience and will not work here.
5. Keep the resulting access token private. Send it in the `Authorization: Bearer <token>` header when testing the local endpoints.

No scopes are required by the current implementation; a valid token for this API is sufficient. CRUD authorization is currently all-or-nothing.

### Try the API with PowerShell

After starting the API and copying an access token from the Auth0 API **Test** tab, set the token in the current PowerShell session and create a record:

```powershell
$token = "PASTE_ACCESS_TOKEN_HERE"
$headers = @{ Authorization = "Bearer $token" }
$stock = Invoke-RestMethod -Method Post -Uri "http://localhost:5080/api/stocks" -Headers $headers -ContentType "application/json" -Body '{"symbol":"DEMO","name":"Demo stock"}'
$stock
```

Use `$stock.id` with `GET`, `PUT`, or `DELETE` on `/api/stocks/{id}`. For example, list all records with:

```powershell
Invoke-RestMethod -Uri "http://localhost:5080/api/stocks" -Headers $headers
```

### Local Run

The project has the tenant domain and suggested audience in `appsettings.json`. Override these with `Auth0__Domain` and `Auth0__Audience` environment variables if your Auth0 values differ. Run from the repository root:

```powershell
dotnet run --project Stock.Api/Stock.Api.csproj
```

The API listens at `http://localhost:5080` when run with its HTTP launch profile. Its collection route is `http://localhost:5080/api/stocks`.

## Acceptance Criteria

- The standalone API builds with the solution.
- All CRUD routes require valid Auth0 JWT bearer tokens.
- The endpoint slices and DTOs are organized according to the repository backend style guide.
- Auth0 setup and the temporary in-memory behavior are documented for local testing.
