using MediatR;
using ProductService.Application.Abstractions.Products.Queries;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Products.QueryHandlers;

public class GetAllProductsHandler : IRequestHandler<GetAllProducts, IEnumerable<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<IEnumerable<Product>> Handle(GetAllProducts request, CancellationToken cancellationToken)
    {
        // TODO: make it async
        return Task.FromResult(_productRepository.GetAllProducts());
    }
}