using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ThunderPay.Domain.Dtos;
using ThunderPay.Domain.UseCases;
using ThunderPay.Server.Abstractions;

namespace ThunderPay.Server.Endpoints.Organization;

public class CreateUpdateOrganization : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("organizations/", async ([FromBody]OrganizationDto dto, IOrganizationUseCases useCases) =>
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
