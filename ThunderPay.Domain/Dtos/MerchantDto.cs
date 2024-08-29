using System.ComponentModel.DataAnnotations;
using ThunderPay.Entities.Enums;

namespace ThunderPay.Domain.Dtos;
public class MerchantDto
{
    public int? Id { get; set; }

    public MerchantStatus? Status { get; set; }

    [Required]
    public required string DisplayName { get; set; }

    [Required]
    public required string UniqueId { get; set; }

    [Required]
    public int OrganizationId { get; set; }
}
