using Domain.Entities.Common;

namespace ProductService.Domain.Entities;

public class InventorySale : BaseEntity
{
    public int ProductId { get; set; }
    public int StoreId { get; set; }
    public DateTime Date { get; set; }
    public int SalesQuantity { get; set; }
    public int Stock { get; set; }
}