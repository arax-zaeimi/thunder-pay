using ThunderPay.Domain.Queriers;
using ThunderPay.Server.Abstractions;

namespace ThunderPay.Server.Endpoints.Organization;

public class GetOrganizations : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("organizations/", async (IOrganizationQuerier querier) =>
        {
            var organizations = await querier.GetAll();

            return Results.Ok(organizations);
        });
    }
}
