using Microsoft.EntityFrameworkCore;
using ThunderPay.Application.Mappers;
using ThunderPay.Database;
using ThunderPay.Domain.Dtos;
using ThunderPay.Domain.Queriers;

namespace ThunderPay.Application.Queriers;
public class OrganizationQuerier(ThunderPayDbContext dbContext) : IOrganizationQuerier
{
    public async Task<OrganizationDto> GetById(int id)
    {
        var organization = await dbContext.Organizations.FirstAsync(q => q.Id == id);
        return OrganizationDtoMapper.Map(organization);
    }
}
