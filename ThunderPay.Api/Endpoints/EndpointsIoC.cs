using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using ThunderPay.Api.Abstractions;
using ThunderPay.Api.OpenApi;

namespace ThunderPay.Api.Endpoints;

public static class EndpointsIoC
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddApiExplorer(options => 
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        services.ConfigureOptions<ConfigureSwaggerGenOptions>();
        
        RegisterEndpointsFromAssembly(services, assembly);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {

        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        RouteGroupBuilder versionedGroup = app
            .MapGroup("/api/v{apiVersion:apiVersion}")
            .WithApiVersionSet(apiVersionSet);

        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(versionedGroup);
        }

        ConfigureSwagger(app);

        return app;
    }

    private static void ConfigureSwagger(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var apiVersionDescriptions = app.DescribeApiVersions();
                foreach (var apiVersionDescription in apiVersionDescriptions)
                {
                    var url = $"/swagger/{apiVersionDescription.GroupName}/swagger.json";
                    var name = apiVersionDescription.GroupName.ToUpperInvariant();

                    options.SwaggerEndpoint(url, name);
                }
            });
        }
    }

    private static void RegisterEndpointsFromAssembly(IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
                    .DefinedTypes
                    .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                                   type.IsAssignableTo(typeof(IEndpoint)))
                    .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                    .ToArray();

        services.TryAddEnumerable(serviceDescriptors);
    }
}
