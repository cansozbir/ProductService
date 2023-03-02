namespace Application.Exceptions;

public class ProductNotFoundException: Exception
{
    public ProductNotFoundException()
    {
    }
    
    public ProductNotFoundException(int storeId) : base("Product with id " + storeId + " not found.")
    {
    }

    public ProductNotFoundException(int storeId, Exception innerException) : base("Product with id " + storeId + " not found.", innerException)
    {
    }
}