using System.ComponentModel.DataAnnotations.Schema;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThunderPay.Database.Sagas.EftSubmission;

[Table("eftSubmissionSagaState")]
public class EftSubmissionSagaStateDbm : TimeStampedEntity, SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }

    public string CurrentState { get; set; } = default!;

    // Postgres optimistic concurrency via xmin
    public uint RowVersion { get; set; }
}

public class EftSubmissionSagaStateMap : SagaClassMap<EftSubmissionSagaStateDbm>
{
    protected override void Configure(EntityTypeBuilder<EftSubmissionSagaStateDbm> entity, ModelBuilder model)
    {
        model.HasDefaultSchema(DatabaseConsts.SagaDefaultSchema);

        entity.Property(x => x.CurrentState).HasMaxLength(64).IsRequired();
        entity.Property(x => x.RowVersion)
              .HasColumnName("xmin")
              .HasColumnType("xid")
              .IsRowVersion();
    }
}
