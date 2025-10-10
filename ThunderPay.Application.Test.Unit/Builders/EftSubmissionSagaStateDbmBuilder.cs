using ThunderPay.Database.Sagas.EftSubmission;

namespace ThunderPay.Application.Test.Unit.Builders;

public class EftSubmissionSagaStateDbmBuilder
{
    private Guid correlationId = Guid.NewGuid();
    private string currentState = "Initial";

    public static EftSubmissionSagaStateDbmBuilder Create()
    {
        return new EftSubmissionSagaStateDbmBuilder();
    }

    public EftSubmissionSagaStateDbmBuilder WithCorrelationId(Guid correlationId)
    {
        this.correlationId = correlationId;
        return this;
    }

    public EftSubmissionSagaStateDbmBuilder WithCurrentState(string currentState)
    {
        this.currentState = currentState;
        return this;
    }

    public EftSubmissionSagaStateDbm Build()
    {
        return new EftSubmissionSagaStateDbm
        {
            CorrelationId = this.correlationId,
            CurrentState = this.currentState,
        };
    }
}