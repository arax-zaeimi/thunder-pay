using ThunderPay.Database;

namespace ThunderPay.ApiService;
public class Program
{
    private static void Main(string[] args)
    {
        DotNetEnv.Env.Load();

        var builder = WebApplication.CreateBuilder(args);

        // Add service defaults & Aspire components.
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddProblemDetails();

        DatabaseIoC.RegisterDatabaseServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseExceptionHandler();

        app.MapDefaultEndpoints();

        DatabaseIoC.Configure(app.Services);

        app.Run();
    }
}