using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions;

public interface IStoreRepository
{
    IEnumerable<Store> GetAllStores();
    Task<Store> GetStoreById(int id);
    Task<Store> CreateStore(Store store);
    Task<Store> UpdateStore(int id, Store store);
    Task DeleteStore(int id);
}