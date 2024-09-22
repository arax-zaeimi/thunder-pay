using ThunderPay.Api;
using ThunderPay.Application;
using ThunderPay.Database;

namespace ThunderPay.Server;
public class Program
{
    private static void Main(string[] args)
    {
        DotNetEnv.Env.Load();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddProblemDetails();

        DatabaseIoC.RegisterDatabaseServices(builder.Services);
        ApplicationIoC.RegisterServices(builder.Services);

        EndpointsIoC.AddEndpoints(builder.Services, typeof(Program).Assembly);

        var app = builder.Build();

        app.UseExceptionHandler();

        DatabaseIoC.Initialize(app.Services);

        EndpointsIoC.MapEndpoints(app);

        app.Run();
    }
}