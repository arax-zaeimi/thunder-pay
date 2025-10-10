using ThunderPay.Application.Sagas.EftSubmission.Messages;

namespace ThunderPay.Application.Test.Unit.Builders;

public class PostTransactionToWalletMsgBuilder
{
    private Guid transactionId = Guid.NewGuid();

    public static PostTransactionToWalletMsgBuilder Create()
    {
        return new PostTransactionToWalletMsgBuilder();
    }

    public PostTransactionToWalletMsgBuilder WithTransactionId(Guid transactionId)
    {
        this.transactionId = transactionId;
        return this;
    }

    public PostTransactionToWalletMsg Build()
    {
        return new PostTransactionToWalletMsg
        {
            TransactionId = this.transactionId,
        };
    }
}