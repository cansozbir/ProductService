using Domain.Entities.Common;

namespace ProductService.Domain.Entities;

public class Product: BaseEntity
{
    public string ProductName { get; set; }
    public int Cost { get; set; }
    public int SalesPrice { get; set; }
}