using ThunderPay.Domain.Dtos;
using ThunderPay.Domain.Entities;

namespace ThunderPay.Application.Mappers;
public static class OrganizationDtoMapper
{
    public static OrganizationDto Map(OrganizationDbm dbm)
    {
        return new OrganizationDto()
        {
            Id = dbm.Id,
            Name = dbm.Name,
        };
    }
}
