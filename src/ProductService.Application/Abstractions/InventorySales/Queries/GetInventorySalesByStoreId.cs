using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.InventorySales.Queries;

public class GetInventorySalesByStoreId: IRequest<IEnumerable<InventorySale>>
{
    public int StoreId { get; set; }
}
