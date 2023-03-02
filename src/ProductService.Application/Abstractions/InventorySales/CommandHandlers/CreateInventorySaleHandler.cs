using MediatR;
using ProductService.Application.Abstractions.InventorySales.Commands;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.InventorySales.CommandHandlers;

public class CreateInventorySaleHandler : IRequestHandler<CreateInventorySale, InventorySale>
{
    private readonly IInventorySaleRepository _inventorySaleRepository;

    public CreateInventorySaleHandler(IInventorySaleRepository inventorySaleRepository)
    {
        _inventorySaleRepository = inventorySaleRepository;
    }

    public async Task<InventorySale> Handle(CreateInventorySale request, CancellationToken cancellationToken)
    {
        var inventorySale = new InventorySale
        {
            ProductId = request.ProductId,
            StoreId = request.StoreId,
            Date = request.Date,
            SalesQuantity = request.SalesQuantity,
            Stock = request.Stock
        };

        return await _inventorySaleRepository.CreateInventorySale(inventorySale);
    }
}