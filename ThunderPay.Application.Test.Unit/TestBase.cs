using MassTransit.Testing;

namespace ThunderPay.Application.Test.Unit;

public class TestBase
{
    public TestBase()
    {
        this.Harness = new InMemoryTestHarness();
    }

    protected InMemoryTestHarness Harness { get; private set; }

    public async Task Initialize()
    {
        await this.Harness.Start();
    }

    public async Task Cleanup()
    {
        await this.Harness.Stop();
    }
}