namespace Application.Exceptions;

public class ProductAlreadyExistsException: Exception
{
    public ProductAlreadyExistsException()
    {
    }
    
    public ProductAlreadyExistsException(int storeId) : base("Product with id " + storeId + " already exists.")
    {
    }

    public ProductAlreadyExistsException(int storeId, Exception innerException) : base("Product with id " + storeId + " already exists.", innerException)
    {
    }
}