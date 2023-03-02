using MediatR;
using ProductService.Application.Abstractions.Products.Queries;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Products.QueryHandlers;

public class GetProductByIdHandler : IRequestHandler<GetProductById, Product>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(GetProductById request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetProductById(request.Id);
    }
}