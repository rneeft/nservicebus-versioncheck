using Messages;
using Microsoft.Extensions.Logging;

namespace Sales;

public class PlaceOrderV2Handler(ILogger<PlaceOrderHandler> logger) :
    IHandleMessages<PlaceOrderV2>
{
    public Task Handle(PlaceOrderV2 message, IMessageHandlerContext context)
    {
        logger.LogInformation("Received PlaceOrder - V2");
        logger.LogInformation("         OrderId = {orderId}", message.OrderId);
        logger.LogInformation("         OrderId = {CustomerId}", message.CustomerId);
        return Task.CompletedTask;
    }
}