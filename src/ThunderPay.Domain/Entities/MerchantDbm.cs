using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThunderPay.Entities.Enums;

namespace ThunderPay.Domain.Entities;

[Table("merchants")]
public class MerchantDbm
{
    [Key]
    public int Id { get; set; }

    public MerchantStatus Status { get; set; }

    [Required]
    public string DisplayName { get; set; } = null!;

    [Required]
    public string UniqueId { get; set; } = null!;

    [Required]
    public int OrganizationId { get; set; }

    public OrganizationDbm? Organization { get; set; }

    public static void Configure(EntityTypeBuilder<MerchantDbm> builder)
    {
        builder
            .HasOne(q => q.Organization)
            .WithMany()
            .HasForeignKey(q => q.OrganizationId);

        builder.HasIndex(q => q.UniqueId).IsUnique();
        builder.HasIndex(q => new { q.OrganizationId, q.DisplayName }).IsUnique();

        builder.Property(q => q.Status).HasConversion<string>();
    }
}
