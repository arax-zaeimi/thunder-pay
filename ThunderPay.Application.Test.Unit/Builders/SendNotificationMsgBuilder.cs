using ThunderPay.Application.Sagas.EftSubmission.Messages;

namespace ThunderPay.Application.Test.Unit.Builders;

public class SendNotificationMsgBuilder
{
    private Guid transactionId = Guid.NewGuid();

    public static SendNotificationMsgBuilder Create()
    {
        return new SendNotificationMsgBuilder();
    }

    public SendNotificationMsgBuilder WithTransactionId(Guid transactionId)
    {
        this.transactionId = transactionId;
        return this;
    }

    public SendNotificationMsg Build()
    {
        return new SendNotificationMsg
        {
            TransactionId = this.transactionId,
        };
    }
}