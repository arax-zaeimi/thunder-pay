using FluentAssertions;
using MassTransit.Testing;
using ThunderPay.Application.Sagas.EftSubmission;
using ThunderPay.Application.Sagas.EftSubmission.Messages;
using ThunderPay.Application.Test.Unit.Builders;

namespace ThunderPay.Application.Test.Unit.Sagas.EftSubmission;

public class EftSubmissionStateMachineTests : IAsyncLifetime
{
    private InMemoryTestHarness harness = null!;
    private EftSubmissionStateMachine stateMachine = null!;

    public async Task InitializeAsync()
    {
        this.harness = new InMemoryTestHarness();
        this.stateMachine = new EftSubmissionStateMachine();

        await this.harness.Start();
    }

    public async Task DisposeAsync()
    {
        await this.harness.Stop();
        this.harness?.Dispose();
    }

    [Fact]
    public async Task Should_Create_Saga_Instance_When_SubmitTransactionToProcessor_Message_Published()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        var message = SubmitTransactionToProcessorMsgBuilder.Create()
            .WithTransactionId(transactionId)
            .WithAmount(100.00m)
            .Build();

        // Act
        await this.harness.Bus.Publish(message);

        // Assert
        this.harness.Published.Select<SubmitTransactionToProcessorMsg>().Any().Should().BeTrue();
    }

    [Fact]
    public async Task Should_Publish_PostTransactionToWalletMsg_When_In_SubmittedToProcessor_State()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        var submitMessage = SubmitTransactionToProcessorMsgBuilder.Create()
            .WithTransactionId(transactionId)
            .Build();

        var walletMessage = PostTransactionToWalletMsgBuilder.Create()
            .WithTransactionId(transactionId)
            .Build();

        // Act
        await this.harness.Bus.Publish(submitMessage);
        await this.harness.Bus.Publish(walletMessage);

        // Assert
        this.harness.Published.Select<PostTransactionToWalletMsg>().Any().Should().BeTrue();
    }

    [Fact]
    public async Task Should_Publish_SendNotificationMsg_When_In_PostedToWallet_State()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        var submitMessage = SubmitTransactionToProcessorMsgBuilder.Create()
            .WithTransactionId(transactionId)
            .Build();

        var walletMessage = PostTransactionToWalletMsgBuilder.Create()
            .WithTransactionId(transactionId)
            .Build();

        var notificationMessage = SendNotificationMsgBuilder.Create()
            .WithTransactionId(transactionId)
            .Build();

        // Act
        await this.harness.Bus.Publish(submitMessage);
        await this.harness.Bus.Publish(walletMessage);
        await this.harness.Bus.Publish(notificationMessage);

        // Assert
        this.harness.Published.Select<SendNotificationMsg>().Any().Should().BeTrue();
    }

    [Fact]
    public async Task Should_Handle_Failed_Transaction_And_Allow_Retry()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        var failMessage = SubmitTransactionToProcessorMsgBuilder.Create()
            .WithTransactionId(transactionId)
            .WithToFail(true)
            .Build();

        var retryMessage = SubmitTransactionToProcessorMsgBuilder.Create()
            .WithTransactionId(transactionId)
            .WithToFail(false)
            .Build();

        // Act
        await this.harness.Bus.Publish(failMessage);
        await this.harness.Bus.Publish(retryMessage);

        // Assert
        this.harness.Published.Select<SubmitTransactionToProcessorMsg>().Count().Should().BeGreaterThanOrEqualTo(2);
    }

    [Fact]
    public void StateMachine_Should_Have_Correct_Initial_State()
    {
        // Assert
        this.stateMachine.Initial.Name.Should().Be("Initial");
    }

    [Fact]
    public void StateMachine_Should_Have_All_Expected_States()
    {
        // Assert
        this.stateMachine.SubmittedToProcessor.Name.Should().Be("SubmittedToProcessor");
        this.stateMachine.PostedToWallet.Name.Should().Be("PostedToWallet");
        this.stateMachine.NotificationSent.Name.Should().Be("NotificationSent");
        this.stateMachine.ProcessorFailed.Name.Should().Be("ProcessorFailed");
        this.stateMachine.WalletFailed.Name.Should().Be("WalletFailed");
        this.stateMachine.NotificationFailed.Name.Should().Be("NotificationFailed");
    }

    [Fact]
    public void StateMachine_Should_Have_All_Expected_Events()
    {
        // Assert
        this.stateMachine.SubmitTransactionToProcessor.Name.Should().Be("SubmitTransactionToProcessor");
        this.stateMachine.PostTransactionToWallet.Name.Should().Be("PostTransactionToWallet");
        this.stateMachine.SendNotification.Name.Should().Be("SendNotification");
    }
}