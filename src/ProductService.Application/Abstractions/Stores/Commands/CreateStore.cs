using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Stores.Commands;

public class CreateStore : IRequest<Store>
{
    public string StoreName { get; set; }
}