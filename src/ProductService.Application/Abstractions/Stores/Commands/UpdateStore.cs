using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Stores.Commands;

public class UpdateStore : IRequest<Store>
{
    public int StoreId { get; set; }
    public string StoreName { get; set; }
}