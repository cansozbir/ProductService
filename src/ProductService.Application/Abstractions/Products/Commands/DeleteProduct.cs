using MediatR;

namespace ProductService.Application.Abstractions.Products.Commands;

public class DeleteProduct : IRequest
{
    public int Id { get; set; }
}