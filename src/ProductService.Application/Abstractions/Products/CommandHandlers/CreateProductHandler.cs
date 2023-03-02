using MediatR;
using ProductService.Application.Abstractions.Products.Commands;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Products.CommandHandlers;

public class CreateProductHandler : IRequestHandler<CreateProduct, Product>
{
    private readonly IProductRepository _productRepository;
    
    public CreateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(CreateProduct request, CancellationToken cancellationToken)
    {
        var newProduct = new Product
        {
            ProductName = request.ProductName,
            Cost = request.Cost,
            SalesPrice = request.SalesPrice
        };
        
        return await _productRepository.CreateProduct(newProduct);
    }
}