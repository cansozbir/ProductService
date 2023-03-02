using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Stores.Queries;

public class GetStoreById : IRequest<Store>
{
    public int StoreId { get; set; }
    
}