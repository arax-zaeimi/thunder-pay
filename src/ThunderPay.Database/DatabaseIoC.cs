using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ThunderPay.Database;

public class DatabaseIoC
{
    public static IServiceCollection RegisterDatabaseServices(IServiceCollection services)
    {
        services.AddDbContext<ThunderPayDbContext>(options =>
            options.UseNpgsql(ConstructDbConnection()));

        return services;
    }

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ThunderPayDbContext>();

        try
        {
            Console.WriteLine($"Pending migrations '{dbContext.Database.GetPendingMigrations().Count()}'");
            dbContext.Database.Migrate();
            Console.WriteLine($"Migrations applied to database. Pending migrations '{dbContext.Database.GetPendingMigrations().Count()}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
            throw;
        }
    }

    private static string ConstructDbConnection()
    {
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
