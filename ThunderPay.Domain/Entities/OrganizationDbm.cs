namespace ThunderPay.Entities.Entities;

public class OrganizationDbm
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<MerchantDbm>? Merchants { get; set; }
}
