using Microsoft.AspNetCore.Http.HttpResults;
using ThunderPay.Api.Abstractions;
using ThunderPay.Domain.Queriers;

namespace ThunderPay.Api.Endpoints.Organization;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/organizations/", async (IOrganizationQuerier querier) =>
        {
            var organizations = await querier.GetAll();

            return Results.Ok(organizations);
        });
    }
}
