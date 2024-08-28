# Start database
```
docker-compose up -d
```

# Add new migration
Run the command in Database project directory.

```
dotnet ef migrations add InitDb --project ThunderPay.Database.csproj --startup-project ../ThunderPay.Api/ThunderPay.Api.csproj
```

