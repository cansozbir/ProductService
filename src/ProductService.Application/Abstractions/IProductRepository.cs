using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions;

public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();
    Task<Product> GetProductById(int id);
    Task<Product> CreateProduct(Product store);
    Task<Product> UpdateProduct(int id, Product store);
    Task DeleteProduct(int id);
    IEnumerable<Product> GetProductsByIds(int[] storeId);
}