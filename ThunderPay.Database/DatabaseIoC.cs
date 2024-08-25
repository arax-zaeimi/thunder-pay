﻿using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ThunderPay.Entities;

namespace ThunderPay.Database;

public class DatabaseIoC
{
    public static IServiceCollection RegisterDatabaseServices(IServiceCollection services)
    {
        

        services.AddDbContext<ThunderPayDbContext>(options =>
            options.UseNpgsql(ConstructDbConnection()));

        return services;
    }

    private static string ConstructDbConnection()
    {
        Env.Load();

        // Retrieve environment variables
        string dbHost = Env.GetString("DB_HOST") ?? throw new Exception("DB_HOST not found");
        string dbPort = Env.GetString("DB_PORT") ?? throw new Exception("DB_PORT not found");
        string dbName = Env.GetString("DB_NAME") ?? throw new Exception("DB_NAME not found");
        string dbUsername = Env.GetString("DB_USERNAME") ?? throw new Exception("DB_USERNAME not found");
        string dbPassword = Env.GetString("DB_PASSWORD") ?? throw new Exception("DB_PASSWORD not found");

        // Construct the connection string
        string connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUsername};Password={dbPassword};";

        return connectionString;
    }    
}
