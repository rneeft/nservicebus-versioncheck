using NServiceBus;
using NServiceBus.MessageMutator;
using NServiceBus.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public static class NServiceBusConfig
    {
        public static EndpointConfiguration Configuration(string name, Version version)
        {
            var connectionString = @"Data Source=.\DEV01;Initial Catalog=nservicebustest;Integrated Security = SSPI;TrustServerCertificate = True;Max Pool Size=100";

            var endpointConfiguration = new EndpointConfiguration(name);

            endpointConfiguration.UseSerialization<SystemJsonSerializer>();
            endpointConfiguration.SendFailedMessagesTo("Error");

            var transport = endpointConfiguration.UseTransport<SqlServerTransport>()
                .ConnectionString(connectionString)
                .DefaultSchema("dbo")
                .Transactions(TransportTransactionMode.SendsAtomicWithReceive);

            endpointConfiguration.RegisterMessageMutator(new VersionMessageMutator(version));

            var recoverability = endpointConfiguration.Recoverability();
            recoverability.CustomPolicy(DelayedRetryAllVersion);

            var delayedDelivery = transport.NativeDelayedDelivery();
            delayedDelivery.TableSuffix("Delayed");

            return endpointConfiguration;
        }

        static RecoverabilityAction DelayedRetryAllVersion(RecoverabilityConfig config, ErrorContext context)
        {
            if (context.Exception is VersionHigherException)
            {
                return RecoverabilityAction.DelayedRetry(TimeSpan.FromSeconds(1));
            }
            else
            {

                // invocation of default recoverability policy
                return DefaultRecoverabilityPolicy.Invoke(config, context);
            }
        }
    }
}
