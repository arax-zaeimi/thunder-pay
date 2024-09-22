using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ThunderPay.Api.Endpoints;
using ThunderPay.Domain.Dtos;
using ThunderPay.Domain.Queriers;
using ThunderPay.Domain.UseCases;

namespace ThunderPay.Server.Endpoints.Organization;

public class Organizations : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("organizations/", async (IOrganizationQuerier querier) =>
        {
            var organizations = await querier.GetAll();

            return Results.Ok(organizations);
        });

        app.MapPost("organizations/", async ([FromBody] OrganizationDto dto, IOrganizationUseCases useCases) =>
        {
            try
            {
                var result = await useCases.Upsert(dto);
                return Results.Ok(result);
            }
            catch (ValidationException ex)
            {
                return Results.ValidationProblem(ex.Errors.ToDictionary(e => e.PropertyName, e => new[] { e.ErrorMessage }));
            }
        });
    }
}
