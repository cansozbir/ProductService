using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.InventorySales.Commands;

public class CreateInventorySale : IRequest<InventorySale>
{
    public int ProductId { get; set; }
    public int StoreId { get; set; }
    public DateTime Date { get; set; }
    public int SalesQuantity { get; set; }
    public int Stock { get; set; }
}