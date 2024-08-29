using System.ComponentModel.DataAnnotations;

namespace ThunderPay.Domain.Dtos;
public class OrganizationDto
{
    public int? Id { get; set; }

    [Required]
    public required string Name { get; set; }
}
