using MediatR;

namespace ProductService.Application.Abstractions.Stores.Queries;

public class GetStoreProfit : IRequest<int>
{
    public int StoreId { get; set; }
}