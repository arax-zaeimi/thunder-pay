using ThunderPay.Application.Sagas.EftSubmission.Messages;

namespace ThunderPay.Application.Test.Unit.Builders;

public class SubmitTransactionToProcessorMsgBuilder
{
    private Guid transactionId = Guid.NewGuid();
    private decimal amount = 100.00m;
    private bool toFail = false;

    public static SubmitTransactionToProcessorMsgBuilder Create()
    {
        return new SubmitTransactionToProcessorMsgBuilder();
    }

    public SubmitTransactionToProcessorMsgBuilder WithTransactionId(Guid transactionId)
    {
        this.transactionId = transactionId;
        return this;
    }

    public SubmitTransactionToProcessorMsgBuilder WithAmount(decimal amount)
    {
        this.amount = amount;
        return this;
    }

    public SubmitTransactionToProcessorMsgBuilder WithToFail(bool toFail)
    {
        this.toFail = toFail;
        return this;
    }

    public SubmitTransactionToProcessorMsg Build()
    {
        return new SubmitTransactionToProcessorMsg
        {
            TransactionId = this.transactionId,
            Amount = this.amount,
            ToFail = this.toFail,
        };
    }
}