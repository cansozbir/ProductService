using MediatR;

namespace ProductService.Application.Abstractions.Stores.Commands;

public class DeleteStore : IRequest
{
    public int StoreId { get; set; }
}