using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions;

public interface IInventorySaleRepository
{
    IEnumerable<InventorySale> GetAllInventorySales();
    Task<InventorySale> GetInventorySaleById(int id);
    Task<InventorySale> CreateInventorySale(InventorySale store);
    Task<InventorySale> UpdateInventorySale(int id, InventorySale store);
    Task DeleteInventorySale(int id);
    IEnumerable<InventorySale> GetInventorySalesByStoreId(int storeId);
    Task<int> GetBestSellerProductIdBySalesQuantity();
    Task DeleteInventorySalesByStoreId(int storeId);
}