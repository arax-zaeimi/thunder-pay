using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using ThunderPay.Database.Sagas.EftSubmission;

namespace ThunderPay.Database.Sagas;

public class PaymentSagaDbContext : SagaDbContext
{
    public PaymentSagaDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get { yield return new EftSubmissionSagaStateMap(); }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in this.ChangeTracker.Entries())
        {
            if (entry.Entity is TimeStampedEntity timeStampedEntity)
            {
                var now = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    timeStampedEntity.CreatedAtUtc = now;
                }

                timeStampedEntity.UpdatedAtUtc = now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
