using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Products.Queries;

public class GetProductById : IRequest<Product>
{
    public int Id { get; set; }
    
}