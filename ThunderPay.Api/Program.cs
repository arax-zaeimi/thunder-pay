using ThunderPay.Api.Endpoints;
using ThunderPay.Application;
using ThunderPay.Database;

namespace ThunderPay.Api;
public class Program
{
    private static void Main(string[] args)
    {
        DotNetEnv.Env.Load();

        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults();

        builder.Services.AddProblemDetails();

        DatabaseIoC.RegisterDatabaseServices(builder.Services);
        ApplicationIoC.RegisterServices(builder.Services);

        EndpointsIoC.AddEndpoints(builder.Services, typeof(Program).Assembly);

        var app = builder.Build();

        app.UseExceptionHandler();
        app.MapDefaultEndpoints();

        DatabaseIoC.Initialize(app.Services);

        EndpointsIoC.MapEndpoints(app);

        app.Run();
    }
}