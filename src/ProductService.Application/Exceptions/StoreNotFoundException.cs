namespace Application.Exceptions;

public class StoreNotFoundException: Exception
{
    public StoreNotFoundException()
    {
    }
    
    public StoreNotFoundException(int storeId) : base("Store with id " + storeId + " not found.")
    {
    }

    public StoreNotFoundException(int storeId, Exception innerException) : base("Store with id " + storeId + " not found.", innerException)
    {
    }
}