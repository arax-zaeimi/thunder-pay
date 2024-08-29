using ThunderPay.Domain.Dtos;

namespace ThunderPay.Domain.UseCases;
public interface IOrganizationUseCases
{
    Task<OrganizationDto> Upsert(OrganizationDto dto);
}
