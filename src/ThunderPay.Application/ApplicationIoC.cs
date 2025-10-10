using DotNetEnv;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.Extensions.DependencyInjection;
using ThunderPay.Application.Sagas.EftSubmission;
using ThunderPay.Database.Sagas;
using ThunderPay.Database.Sagas.EftSubmission;

namespace ThunderPay.Application;
public class ApplicationIoC
{
    public static void RegisterServices(IServiceCollection services)
    {
        RegisterMassTransit(services);
    }

    public static void RegisterMassTransit(IServiceCollection services)
    {
        services.AddMassTransit<IBus>(x =>
        {
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(Env.GetString("AZURE_SERVICE_BUS") ?? throw new Exception("AZURE_SERVICE_BUS"));

                cfg.ConfigureEndpoints(context, KebabCaseEndpointNameFormatter.Instance);

                cfg.ReceiveEndpoint(e =>
                {
                    e.PrefetchCount = 10;
                    e.ConcurrentMessageLimit = 5;
                    e.UseInMemoryOutbox(context);
                });

                // cfg.UseScheduledRedelivery(r => r.Intervals(TimeSpan.FromSeconds(10), TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(5)));
                // cfg.UseMessageRetry(r => r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(5)));
            });

            x.AddSagaStateMachine<EftSubmissionStateMachine, EftSubmissionSagaStateDbm>()
                .Endpoint(e => e.Name = "eft-submission-saga")
                .EntityFrameworkRepository(r =>
                {
                    r.ExistingDbContext<PaymentSagaDbContext>();
                    r.UsePostgres();
                    r.LockStatementProvider = new PostgresLockStatementProvider();
                    r.ConcurrencyMode = ConcurrencyMode.Optimistic;
                    r.IsolationLevel = System.Data.IsolationLevel.ReadCommitted;
                });
        });
    }
}
