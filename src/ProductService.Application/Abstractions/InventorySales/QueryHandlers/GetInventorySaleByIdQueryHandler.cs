using MediatR;
using ProductService.Application.Abstractions.InventorySales.Queries;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.InventorySales.QueryHandlers;

public class GetInventorySaleByIdQueryHandler : IRequestHandler<GetInventorySaleById, InventorySale>
{
    private readonly IInventorySaleRepository _inventorySaleRepository;

    public GetInventorySaleByIdQueryHandler(IInventorySaleRepository inventorySaleRepository)
    {
        _inventorySaleRepository = inventorySaleRepository;
    }

    public async Task<InventorySale> Handle(GetInventorySaleById request, CancellationToken cancellationToken)
    {
        return await _inventorySaleRepository.GetInventorySaleById(request.Id);
    }
}