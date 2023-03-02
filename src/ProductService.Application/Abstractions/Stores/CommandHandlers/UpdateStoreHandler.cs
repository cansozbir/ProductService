using Application.Exceptions;
using MediatR;
using ProductService.Application.Abstractions.Stores.Commands;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Stores.CommandHandlers;

public class UpdateStoreHandler : IRequestHandler<UpdateStore, Store>
{
    private readonly IStoreRepository _storeRepository;
    
    public UpdateStoreHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }
    
    public async Task<Store> Handle(UpdateStore request, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetStoreById(request.StoreId);
        if (store == null)
        {
            throw new StoreNotFoundException(request.StoreId);
        }
        store.StoreName = request.StoreName;
        return await _storeRepository.UpdateStore(request.StoreId, store);
    }
}