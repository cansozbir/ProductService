using MediatR;
using ProductService.Application.Abstractions.Stores.Commands;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Stores.CommandHandlers;

public class CreateStoreHandler : IRequestHandler<CreateStore, Store>
{
    private readonly IStoreRepository _storeRepository;
    
    public CreateStoreHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }
    
    public async Task<Store> Handle(CreateStore request, CancellationToken cancellationToken)
    {
        var newStore = new Store
        {
            StoreName = request.StoreName
        };
        
        return await _storeRepository.CreateStore(newStore);
    }
}