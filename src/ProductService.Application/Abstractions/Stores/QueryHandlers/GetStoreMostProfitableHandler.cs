using Application.Exceptions;
using MediatR;
using ProductService.Application.Abstractions.Stores.Queries;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Stores.QueryHandlers;

public class GetStoreMostProfitableHandler : IRequestHandler<GetStoreMostProfitable, Store>
{
    private readonly IStoreRepository _storeRepository;
    private readonly IProductRepository _productRepository;
    private readonly IInventorySaleRepository _inventorySaleRepository;
    
    public GetStoreMostProfitableHandler(IStoreRepository storeRepository, IProductRepository productRepository, IInventorySaleRepository inventorySaleRepository)
    {
        _storeRepository = storeRepository;
        _productRepository = productRepository;
        _inventorySaleRepository = inventorySaleRepository;
    }

    public async Task<Store?> Handle(GetStoreMostProfitable request, CancellationToken cancellationToken)
    {
        // TODO: Refactor this method, to reduce re-usage of code

        var stores = _storeRepository.GetAllStores().ToList();
        
        if (stores.Count == 0)
        {
            throw new StoreNotFoundException();
        }
        
        var storeProfits = new Dictionary<int, int>();
        
        foreach (var storeId in stores.Select(s => s.Id))
        {
            var profit = 0;
            
            var inventorySales =  _inventorySaleRepository.GetInventorySalesByStoreId(storeId).ToList();
            
            if (inventorySales.Count == 0)
            {
                storeProfits.Add(storeId, profit);
                continue;
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
            
            storeProfits.Add(storeId, profit);
        }
        
        var mostProfitableStoreId = storeProfits.OrderByDescending(s => s.Value).First().Key;
        var store = stores.FirstOrDefault(s => s.Id == mostProfitableStoreId);
        return store;
    }
}