using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Products.Queries;

public class GetAllProducts : IRequest<IEnumerable<Product>>
{
}
