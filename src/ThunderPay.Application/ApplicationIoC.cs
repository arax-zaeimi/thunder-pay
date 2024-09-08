using Microsoft.Extensions.DependencyInjection;
using ThunderPay.Application.Commands.Organization;
using ThunderPay.Application.Queriers;
using ThunderPay.Domain.Queriers;
using ThunderPay.Domain.UseCases;

namespace ThunderPay.Application;
public class ApplicationIoC
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IOrganizationUseCases, OrganizationUseCases>();

        services.AddScoped<IOrganizationQuerier, OrganizationQuerier>();
    }
}
