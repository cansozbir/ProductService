using MediatR;
using ProductService.Application.Abstractions.Stores.Commands;

namespace ProductService.Application.Abstractions.Stores.CommandHandlers;

public class DeleteStoreHandler : IRequestHandler<DeleteStore>
{
    private readonly IStoreRepository _storeRepository;
    private readonly IInventorySaleRepository _inventorySaleRepository;
    
    public DeleteStoreHandler(IStoreRepository storeRepository, IInventorySaleRepository inventorySaleRepository)
    {
        _storeRepository = storeRepository;
        _inventorySaleRepository = inventorySaleRepository;
    }
    
    public async Task Handle(DeleteStore request, CancellationToken cancellationToken)
    {
        await _storeRepository.DeleteStore(request.StoreId);
    }
}
    