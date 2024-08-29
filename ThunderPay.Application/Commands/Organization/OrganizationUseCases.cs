using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using ThunderPay.Application.Validators;
using ThunderPay.Database;
using ThunderPay.Domain.Dtos;
using ThunderPay.Domain.Entities;
using ThunderPay.Domain.Queriers;
using ThunderPay.Domain.UseCases;

namespace ThunderPay.Application.Commands.Organization;

public class OrganizationUseCases(ThunderPayDbContext db, IOrganizationQuerier organizationQuerier) : IOrganizationUseCases
{
    public async Task<OrganizationDto> Upsert(OrganizationDto dto)
    {
        var validationResult = await this.ValidateDto(dto);
        if(!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        OrganizationDbm organization = new OrganizationDbm();
        if(dto.Id > 0)
        {
            organization = await db.Organizations.FirstAsync(q  => q.Id == dto.Id);
        }
        else
        {
            db.Organizations.Add(organization);
        }

        organization.Name = dto.Name;
        await db.SaveChangesAsync();

        return await organizationQuerier.GetById(organization.Id);
    }

    private async Task<ValidationResult> ValidateDto(OrganizationDto dto)
    {
        var validator = new OrganizationDtoValidator();
        var result = validator.Validate(dto);

        var query = db.Organizations.Where(q => q.Name.ToLower() == dto.Name.ToLower());

        if (dto.Id > 0)
        {
            query = query.Where(q => q.Id != dto.Id);
        }

        var exists = await query.AnyAsync();

        if (exists)
        {
            result.Errors.Add(new ValidationFailure(nameof(OrganizationDto.Name), "Organization name already exists."));
        }

        return result;
    }
}
