using Application.Exceptions;
using MediatR;
using ProductService.Application.Abstractions.Stores.Queries;

namespace ProductService.Application.Abstractions.Stores.QueryHandlers;

public class GetStoreProfitHandler : IRequestHandler<GetStoreProfit, int>
{
    private readonly IStoreRepository _storeRepository;
    private readonly IProductRepository _productRepository;
    private readonly IInventorySaleRepository _inventorySaleRepository;

    public GetStoreProfitHandler(IStoreRepository storeRepository, IProductRepository productRepository, IInventorySaleRepository inventorySaleRepository)
    {
        _storeRepository = storeRepository;
        _productRepository = productRepository;
        _inventorySaleRepository = inventorySaleRepository;
    }

    public async Task<int> Handle(GetStoreProfit request, CancellationToken cancellationToken)
    {
        var profit = 0;

        var store = await _storeRepository.GetStoreById(request.StoreId);
        if (store is null)
        {
            throw new StoreNotFoundException(request.StoreId);
        }

        var inventorySales =  _inventorySaleRepository.GetInventorySalesByStoreId(request.StoreId).ToList();

        if (inventorySales.Count == 0)
        {
            return profit;
        }

        var products = _productRepository.GetProductsByIds(inventorySales.Select(s => s.ProductId).Distinct().ToArray()).ToList();

        foreach (var inventorySale in inventorySales)
        {
            var product = products.FirstOrDefault(p => p.Id == inventorySale.ProductId); 
            if (product is null)
            {
                throw new ProductNotFoundException(inventorySale.ProductId);
            }

            profit += (product.SalesPrice - product.Cost) * inventorySale.SalesQuantity;
        }

        return profit;
    }
}