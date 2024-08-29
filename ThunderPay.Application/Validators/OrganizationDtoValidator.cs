using FluentValidation;
using ThunderPay.Domain.Dtos;

namespace ThunderPay.Application.Validators;
public class OrganizationDtoValidator : AbstractValidator<OrganizationDto>
{
    public OrganizationDtoValidator()
    {
        this.RuleFor(q => q.Name).NotEmpty().WithMessage($"{nameof(OrganizationDto.Name)} is required.");
    }
}
