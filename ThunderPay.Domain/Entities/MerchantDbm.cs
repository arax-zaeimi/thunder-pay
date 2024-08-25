using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using ThunderPay.Entities.Enums;

namespace ThunderPay.Entities.Entities;

public class MerchantDbm
{
    [Key]
    public int Id { get; set; }

    public MerchantStatus Status { get; set; }

    [Required]
    public string DisplayName { get; set; } = null!;

    [Required]
    public string UniqueCode { get; set; } = null!;

    [Required]
    public int OrganizationId { get; set; }

    public OrganizationDbm? Organization { get; set; }

    public static void Configure(EntityTypeBuilder<MerchantDbm> builder)
    {
        builder
            .HasOne(q => q.Organization)
            .WithMany()
            .HasForeignKey(q => q.OrganizationId);

        builder.Property(q => q.Status).HasConversion<string>();
    }
}
