using ThunderPay.Domain.Dtos;

namespace ThunderPay.Domain.Queriers;
public interface IOrganizationQuerier
{
    Task<OrganizationDto> GetById(int id);
}
