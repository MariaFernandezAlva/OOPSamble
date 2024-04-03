using OOPSamble.Shared.Domain.Model.ValueObjects;

namespace OOPSamble.Sales.Domain.Model.Aggregates;

public class SalesOrder(int customerId)
{
    //elementos basicos que inicializan
    public Guid Id { get; } = Guid.NewGuid(); //solo de lectura  ->Guid permite generar aleatoriamente claves
    public int CustomerId { get; } = customerId;
    
    //Crear un status
    public SalesOrderStatus Status { get; private set; } = SalesOrderStatus.PandingPayment;
    
    //se le agrega el address
    public Address ShippingAddress { get; private set; }
    public double PaidAmount { get; private set; } = 0;
}