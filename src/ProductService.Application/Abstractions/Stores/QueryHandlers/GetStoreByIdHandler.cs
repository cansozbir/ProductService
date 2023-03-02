using MediatR;
using ProductService.Application.Abstractions.Stores.Queries;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Stores.QueryHandlers;

public class GetStoreByIdHandler : IRequestHandler<GetStoreById, Store>
{
    private readonly IStoreRepository _storeRepository;

    public GetStoreByIdHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<Store> Handle(GetStoreById request, CancellationToken cancellationToken)
    {
        return await _storeRepository.GetStoreById(request.StoreId);
    }
}