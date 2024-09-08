using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ThunderPay.Server.OpenApi;

public class ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    : IConfigureNamedOptions<SwaggerGenOptions>
{
    public void Configure(string? name, SwaggerGenOptions options)
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            var openApiInfo = new OpenApiInfo()
            {
                Title = $"ThunderPay.Api v{description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
            };

            options.SwaggerDoc(description.GroupName, openApiInfo);
        }
    }

    public void Configure(SwaggerGenOptions options)
    {
        this.Configure(options);
    }
}
