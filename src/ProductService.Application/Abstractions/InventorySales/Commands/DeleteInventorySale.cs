using MediatR;

namespace ProductService.Application.Abstractions.InventorySales.Commands;

public class DeleteInventorySale : IRequest
{
    public int Id { get; set; }
}