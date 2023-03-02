using MediatR;
using ProductService.Application.Abstractions.InventorySales.Queries;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.InventorySales.QueryHandlers;

public class GetAllInventorySalesQueryHandler : IRequestHandler<GetAllInventorySales, IEnumerable<InventorySale>>
{
    private readonly IInventorySaleRepository _inventorySaleRepository;

    public GetAllInventorySalesQueryHandler(IInventorySaleRepository inventorySaleRepository)
    {
        _inventorySaleRepository = inventorySaleRepository;
    }

    public async Task<IEnumerable<InventorySale>> Handle(GetAllInventorySales request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_inventorySaleRepository.GetAllInventorySales());
    }
}