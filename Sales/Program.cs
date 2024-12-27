using Microsoft.Extensions.Hosting;
using Messages;

Console.Title = "Sales";

Console.WriteLine("#############################");
Console.WriteLine("### SALES - Version 1.0.0 ###");
Console.WriteLine("#############################");

var builder = Host.CreateApplicationBuilder(args);

var endpointConfiguration = NServiceBusConfig.Configuration("PlaceOrderEndpoint", new Version(1, 0, 0));
builder.UseNServiceBus(endpointConfiguration);

await builder.Build().RunAsync();



