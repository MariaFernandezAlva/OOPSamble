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
    private readonly List<SalesOrderItem> _items = [];

    public void AddItem(int productId, int quantity, double unitPrice)
    {
        if (Status != SalesOrderStatus.PandingPayment)
        {
            throw new InvalidOperationException("Can't modify an order once payment is processed.");
        }
        _items.Add(new SalesOrderItem(Id,productId,quantity,unitPrice));
    }
    //cancelar la orden
    public void Cancel()
    {
        Status = SalesOrderStatus.Cancelled;
    }

    public void Dispatch(string street, string city, string state, string zipcode, string country)
    {
        if (Status == SalesOrderStatus.PandingPayment)
        {
            throw new InvalidOperationException("Can't dispatch and order that is not yet.");
        }

        if (_items.Count == 0)
        {
            throw new InvalidOperationException("Can't dispatch and order without items.");
        }

        ShippingAddress = new Address(street, city, state, zipcode, country);
        Status = SalesOrderStatus.Shipped;
    }

    public double CalculateTotalPrice() => _items.Sum(item => item.CalculateItemPrice());

    public void AddPayment(double amount)
    {
        if (amount <= 0)
        {
            throw new InvalidOperationException("Amount must be greater than zero.");
        }

        if (amount > CalculateTotalPrice() - PaidAmount)
        {
            throw new InvalidOperationException("Amount must be less than or equal to the total price.");
        }

        PaidAmount += amount;
    }
    
}