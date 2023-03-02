namespace Application.Exceptions;

public class SaleNotFoundException: Exception
{
    public SaleNotFoundException() : base("Sale not found.")
    {
    }
    
    public SaleNotFoundException(int saleId) : base("Sale with id " + saleId + " not found.")
    {
    }

    public SaleNotFoundException(int saleId, Exception innerException) : base("Sale with id " + saleId + " not fount.", innerException)
    {
    }
}