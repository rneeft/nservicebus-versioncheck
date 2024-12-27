namespace Messages;

public class PlaceOrderV2 :
    ICommand
{
    public string OrderId { get; set; }
    public string CustomerId { get; set; }
}

