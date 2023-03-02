using MediatR;
using ProductService.Application.Abstractions.Stores.Queries;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Stores.QueryHandlers;

public class GetAllStoresHandler : IRequestHandler<GetAllStores, IEnumerable<Store>>
{
    private readonly IStoreRepository _storeRepository;

    public GetAllStoresHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<IEnumerable<Store>> Handle(GetAllStores request, CancellationToken cancellationToken)
    {
        // TODO: make it async
        return await Task.FromResult(_storeRepository.GetAllStores());
    }
}