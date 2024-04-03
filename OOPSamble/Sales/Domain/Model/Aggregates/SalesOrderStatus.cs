namespace OOPSamble.Sales.Domain.Model.Aggregates;

public enum SalesOrderStatus
{
    //Preparo los estados de una orden de ventas
    Cancelled,
    PandingPayment,
    ReadyForShipment,
    Shipped,
    Completed
}