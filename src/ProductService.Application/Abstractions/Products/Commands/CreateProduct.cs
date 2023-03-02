using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Products.Commands;

public class CreateProduct : IRequest<Product>
{
    public string ProductName { get; set; }
    public int Cost { get; set; }
    public int SalesPrice { get; set; }
}