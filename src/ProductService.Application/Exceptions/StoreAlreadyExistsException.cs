namespace Application.Exceptions;

public class StoreAlreadyExistsException: Exception
{
    public StoreAlreadyExistsException()
    {
    }
    
    public StoreAlreadyExistsException(int storeId) : base("Store with id " + storeId + " already exists.")
    {
    }

    public StoreAlreadyExistsException(int storeId, Exception innerException) : base("Store with id " + storeId + " already exists.", innerException)
    {
    }
}