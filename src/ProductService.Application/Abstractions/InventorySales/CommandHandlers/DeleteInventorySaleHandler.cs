using MediatR;
using ProductService.Application.Abstractions.InventorySales.Commands;

namespace ProductService.Application.Abstractions.InventorySales.CommandHandlers;

public class DeleteInventorySaleHandler : IRequestHandler<DeleteInventorySale>
{
    private readonly IInventorySaleRepository _inventorySaleRepository;

    public DeleteInventorySaleHandler(IInventorySaleRepository inventorySaleRepository)
    {
        _inventorySaleRepository = inventorySaleRepository;
    }

    public async Task Handle(DeleteInventorySale request, CancellationToken cancellationToken)
    {
        await _inventorySaleRepository.DeleteInventorySale(request.Id);
    }
}