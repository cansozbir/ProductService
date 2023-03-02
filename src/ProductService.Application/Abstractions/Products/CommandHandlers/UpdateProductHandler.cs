using Application.Exceptions;
using MediatR;
using ProductService.Application.Abstractions.Products.Commands;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Products.CommandHandlers;

public class UpdateProductHandler : IRequestHandler<UpdateProduct, Product>
{
    private readonly IProductRepository _productRepository;
    
    public UpdateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Product> Handle(UpdateProduct request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(request.Id);
        
        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        
        product.ProductName = request.ProductName;
        product.SalesPrice = request.SalesPrice;
        product.Cost = request.Cost;
        
        return await _productRepository.UpdateProduct(request.Id, product);
    }
}