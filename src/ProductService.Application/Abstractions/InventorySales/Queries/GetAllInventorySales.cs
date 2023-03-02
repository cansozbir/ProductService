using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.InventorySales.Queries;

public class GetAllInventorySales: IRequest<IEnumerable<InventorySale>>
{
}