﻿using Messages;
using Microsoft.Extensions.Logging;

namespace Sales;

public class PlaceOrderHandler(ILogger<PlaceOrderHandler> logger) :
    IHandleMessages<PlaceOrder>
{
    public Task Handle(PlaceOrder message, IMessageHandlerContext context)
    {
        logger.LogInformation("Received PlaceOrder");
        logger.LogInformation("         OrderId = {orderId}", message.OrderId);
        return Task.CompletedTask;
    }
}
