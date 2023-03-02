using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.InventorySales.Queries;

public class GetInventorySaleById : IRequest<InventorySale>
{
    public int Id { get; set; }
}