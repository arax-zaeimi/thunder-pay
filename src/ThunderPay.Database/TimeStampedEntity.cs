namespace ThunderPay.Database;

public abstract class TimeStampedEntity
{
    public DateTime CreatedAtUtc { get; set; }

    public DateTime UpdatedAtUtc { get; set; }
}
