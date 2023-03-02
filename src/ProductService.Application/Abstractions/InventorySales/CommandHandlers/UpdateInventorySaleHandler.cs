using Application.Exceptions;
using MediatR;
using ProductService.Application.Abstractions.InventorySales.Commands;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.InventorySales.CommandHandlers;

public class UpdateInventorySaleHandler : IRequestHandler<UpdateInventorySale, InventorySale>
{
    private readonly IInventorySaleRepository _inventorySaleRepository;

    public UpdateInventorySaleHandler(IInventorySaleRepository inventorySaleRepository)
    {
        _inventorySaleRepository = inventorySaleRepository;
    }

    public async Task<InventorySale> Handle(UpdateInventorySale request, CancellationToken cancellationToken)
    {
        var inventorySale = await _inventorySaleRepository.GetInventorySaleById(request.Id);

        if (inventorySale == null)
        {
            throw new SaleNotFoundException(request.Id);
        }

        inventorySale.ProductId = request.ProductId;
        inventorySale.StoreId = request.StoreId;
        inventorySale.Date = request.Date;
        inventorySale.SalesQuantity = request.SalesQuantity;
        inventorySale.Stock = request.Stock;

        return await _inventorySaleRepository.UpdateInventorySale(request.Id, inventorySale);
    }
}