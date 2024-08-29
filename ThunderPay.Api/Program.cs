using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Builder;
using Microsoft.OpenApi.Models;
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

        // Add service defaults & Aspire components.
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddProblemDetails();

        DatabaseIoC.RegisterDatabaseServices(builder.Services);
        ApplicationIoC.RegisterServices(builder.Services);
        EndpointsIoC.AddEndpoints(builder.Services, typeof(Program).Assembly);

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddApiVersioning(options =>
        {
            // Default API version (when no version is specified)
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;

            // Versioning by URL segment (e.g., /v1/resource)
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        builder.Services.AddEndpointsApiExplorer();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseExceptionHandler();
        app.MapDefaultEndpoints();

        ApiVersionSet apiVersionSet = app
            .NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        RouteGroupBuilder versionedGroup = app
            .MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet);

        DatabaseIoC.Initialize(app.Services);
        EndpointsIoC.MapEndpoints(app, versionedGroup);

        app.Run();
    }
}