

# Start database
```
docker-compose up -d
```

# Add new migration
Run the commands from the solution root directory.

```
dotnet ef migrations add InitDb --project src/ThunderPay.Database/ThunderPay.Database.csproj --startup-project src/ThunderPay.Api/ThunderPay.Api.csproj

dotnet ef migrations add Init_Saga --context PaymentSagaDbContext --project src/ThunderPay.Database/ThunderPay.Database.csproj --startup-project src/ThunderPay.Api/ThunderPay.Api.csproj
```
