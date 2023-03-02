using MediatR;
using ProductService.Application.Abstractions.Products.Commands;

namespace ProductService.Application.Abstractions.Products.CommandHandlers;

public class DeleteProductHandler : IRequestHandler<DeleteProduct>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductHandler(IProductRepository productRepository)
    {
        _productRepository= productRepository;
    }

    public async Task Handle(DeleteProduct request, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteProduct(request.Id);
    }
}