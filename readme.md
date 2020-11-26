# Simple Email API Service

## Stack

- .Net 5
- xUnit
- MSSQL
- Serilog

## Config

Configure your own connection string in appsetings.json
then type:

```powershell
dotnet ef database update
```

Type in console:

```powershell
dotnet user-secrets set "Email:server" "smtp.gmail.com"
dotnet user-secrets set "Email:port" "587"
dotnet user-secrets set "Email:email" "email"
dotnet user-secrets set "Email:password" "password"
```
