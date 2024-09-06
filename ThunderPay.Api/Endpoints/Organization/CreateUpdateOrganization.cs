using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ThunderPay.Api.Abstractions;
using ThunderPay.Domain.Dtos;
using ThunderPay.Domain.UseCases;

namespace ThunderPay.Api.Endpoints.Organization;

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
            catch(ValidationException ex)
            {
                return Results.ValidationProblem(ex.Errors.ToDictionary(e => e.PropertyName, e => new[] { e.ErrorMessage }));
            }
        });
    }
}
