namespace OOPSamble.Sales.Domain.Model.Aggregates;

public class SalesOrder(int customerId)
{
    //elementos basicos que inicializan
    public Guid Id { get; } = Guid.NewGuid(); //solo de lectura  ->Guid permite generar aleatoriamente claves
    public int CustomerId { get; } = customerId;
}