using ClientUI;
using Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus.Features;

Console.Title = "ClientUI";

var builder = Host.CreateApplicationBuilder(args);

var connectionString = @"Data Source=.\DEV01;Initial Catalog=nservicebustest;Integrated Security = SSPI;TrustServerCertificate = True;Max Pool Size=100";

var endpointConfiguration = new EndpointConfiguration("ClientUIEndpoint");
endpointConfiguration.UseSerialization<SystemJsonSerializer>();
endpointConfiguration.DisableFeature<AutoSubscribe>();
endpointConfiguration.DisableFeature<Audit>();
endpointConfiguration.SendFailedMessagesTo("Error");

var transport = endpointConfiguration.UseTransport<SqlServerTransport>()
    .ConnectionString(connectionString)
    .DefaultSchema("dbo")
    .Transactions(TransportTransactionMode.SendsAtomicWithReceive);

var delayedDelivery = transport.NativeDelayedDelivery();
delayedDelivery.TableSuffix("Delayed");

var routing = transport.Routing();

routing.RouteToEndpoint(typeof(PlaceOrder), "PlaceOrderEndpoint");
routing.RouteToEndpoint(typeof(PlaceOrderV2), "PlaceOrderEndpoint");

builder.UseNServiceBus(endpointConfiguration);

builder.Services.AddHostedService<InputLoopService>();

await builder.Build().RunAsync();