using Application.Exceptions;
using MediatR;
using ProductService.Application.Abstractions.Stores.Queries;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Stores.QueryHandlers;

public class GetBestSellerProductByQuantityHandler: IRequestHandler<GetBestSellerProductByQuantity, Product>
{
    private readonly IProductRepository _productRepository;
    private readonly IInventorySaleRepository _inventorySaleRepository;

    public GetBestSellerProductByQuantityHandler(IProductRepository productRepository, IInventorySaleRepository inventorySaleRepository)
    {
        _productRepository = productRepository;
        _inventorySaleRepository = inventorySaleRepository;
    }

    public async Task<Product> Handle(GetBestSellerProductByQuantity request, CancellationToken cancellationToken)
    {
        var productId = await _inventorySaleRepository.GetBestSellerProductIdBySalesQuantity();
        return await _productRepository.GetProductById(productId);
    }
}