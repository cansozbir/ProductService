using MediatR;
using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Stores.Queries;

public class GetAllStores : IRequest<IEnumerable<Store>>
{
}
