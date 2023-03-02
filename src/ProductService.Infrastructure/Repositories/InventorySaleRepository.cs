using Application.Exceptions;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ProductService.Application.Abstractions;
using ProductService.Domain.Entities;

namespace Infrastructure.Repositories;

public class InventorySaleRepository : IInventorySaleRepository
{
    private readonly string? _connectionString;

    public InventorySaleRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<InventorySale> GetAllInventorySales()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("SELECT * FROM \"InventorySales\"", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        yield return new InventorySale
                        {
                            Id = reader.GetInt32(0),
                            ProductId = reader.GetInt32(1),
                            StoreId = reader.GetInt32(2),
                            Date = reader.GetDateTime(3),
                            SalesQuantity = reader.GetInt32(4),
                            Stock = reader.GetInt32(5)
                        };
                }
            }
        }
    }

    public Task<InventorySale> GetInventorySaleById(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("SELECT * FROM \"InventorySales\" WHERE \"Id\" = @id", connection))
            {
                command.Parameters.AddWithValue("id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return Task.FromResult(new InventorySale
                        {
                            Id = reader.GetInt32(0),
                            ProductId = reader.GetInt32(1),
                            StoreId = reader.GetInt32(2),
                            Date = reader.GetDateTime(3),
                            SalesQuantity = reader.GetInt32(4),
                            Stock = reader.GetInt32(5)
                        });
                }
            }
        }

        throw new SaleNotFoundException(id);
    }

    public Task<InventorySale> CreateInventorySale(InventorySale product)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command =
                   new NpgsqlCommand(
                       "INSERT INTO \"InventorySales\" (\"ProductId\", \"StoreId\", \"Date\", \"SalesQuantity\", \"Stock\") VALUES (@ProductId, @StoreId, @Date, @SalesQuantity, @Stock) RETURNING \"Id\"",
                       connection))
            {
                command.Parameters.AddWithValue("ProductId", product.ProductId);
                command.Parameters.AddWithValue("StoreId", product.StoreId);
                command.Parameters.AddWithValue("Date", product.Date);
                command.Parameters.AddWithValue("SalesQuantity", product.SalesQuantity);
                command.Parameters.AddWithValue("Stock", product.Stock);
                product.Id = (int) command.ExecuteScalar();
                
                return Task.FromResult(product);
            }
        }
    }

    public Task<InventorySale> UpdateInventorySale(int id, InventorySale product)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command =
                   new NpgsqlCommand(
                       "UPDATE \"InventorySales\" SET \"ProductId\" = @ProductId, \"StoreId\" = @StoreId, \"Date\" = @Date, \"SalesQuantity\" = @SalesQuantity, \"Stock\" = @Stock WHERE \"Id\" = @Id",
                       connection))
            {
                command.Parameters.AddWithValue("Id", id);
                command.Parameters.AddWithValue("ProductId", product.ProductId);
                command.Parameters.AddWithValue("StoreId", product.StoreId);
                command.Parameters.AddWithValue("Date", product.Date);
                command.Parameters.AddWithValue("SalesQuantity", product.SalesQuantity);
                command.Parameters.AddWithValue("Stock", product.Stock);
                command.ExecuteNonQuery();
            }
            return Task.FromResult(product);
        }
    }

    public Task DeleteInventorySale(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("DELETE FROM \"InventorySales\" WHERE \"Id\" = @id", connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
            }

            return Task.CompletedTask;
        }
    }
    
    public IEnumerable<InventorySale> GetInventorySalesByStoreId(int storeId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("SELECT * FROM \"InventorySales\" WHERE \"StoreId\" = @storeId", connection))
            {
                command.Parameters.AddWithValue("storeId", storeId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        yield return new InventorySale
                        {
                            Id = reader.GetInt32(0),
                            ProductId = reader.GetInt32(1),
                            StoreId = reader.GetInt32(2),
                            Date = reader.GetDateTime(3),
                            SalesQuantity = reader.GetInt32(4),
                            Stock = reader.GetInt32(5)
                        };
                }
            }
        }
    }
    
    public Task<int> GetBestSellerProductIdBySalesQuantity()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("SELECT \"ProductId\" FROM \"InventorySales\" GROUP BY \"ProductId\" ORDER BY SUM(\"SalesQuantity\") DESC LIMIT 1", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return Task.FromResult(reader.GetInt32(0));
                }
            }
        }

        throw new SaleNotFoundException();
    }
    
    public Task DeleteInventorySalesByStoreId(int storeId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("DELETE FROM \"InventorySales\" WHERE \"StoreId\" = @storeId", connection))
            {
                command.Parameters.AddWithValue("storeId", storeId);
                command.ExecuteNonQuery();
            }

            return Task.CompletedTask;
        }
    }
}

