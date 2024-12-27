using Messages;
using Microsoft.Extensions.Hosting;

namespace ClientUI;

public class InputLoopService(IMessageSession messageSession) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            Console.WriteLine("Press 'P' to place an order, Press 'N' to place a 'neworder', or 'Q' to quit.");
            var key = Console.ReadKey();
            Console.WriteLine();
            var options = new SendOptions();

            switch (key.Key)
            {
                case ConsoleKey.P:
                    // Instantiate the command
                    var command = new PlaceOrder
                    {
                        OrderId = Guid.NewGuid().ToString()
                    };
                    options.SetHeader("ApplicationVersion", "1.0.0");

                    // Send the command
                    Console.WriteLine($"Sending PlaceOrder command, OrderId = {command.OrderId}");
                    await messageSession.Send(command,options, cancellationToken: stoppingToken);

                    break;

                case ConsoleKey.N:
                    var command2 = new PlaceOrderV2
                    {
                        OrderId = Guid.NewGuid().ToString(),
                        CustomerId = Guid.NewGuid().ToString()
                    };

                    options.SetHeader("ApplicationVersion", "1.1.0");

                    // Send the command
                    Console.WriteLine($"Sending PlaceOrderV2 command, OrderId = {command2.OrderId}");
                    await messageSession.Send(command2, options, cancellationToken: stoppingToken);
                    break;

                case ConsoleKey.Q:
                    return;

                default:
                    Console.WriteLine("Unknown input. Please try again.");
                    break;
            }
        }
    }
}