using ThunderPay.Domain.Dtos;

namespace ThunderPay.Domain.Queriers;
public interface IOrganizationQuerier
{
    Task<List<OrganizationDto>> GetAll();
    Task<OrganizationDto> GetById(int id);
}
