using Application.Exceptions;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ProductService.Application.Abstractions;
using ProductService.Domain.Entities;

namespace ProductService.Presentation.Repositories.StoreRepository;

public class StoreRepository : IStoreRepository
{
    private readonly string? _connectionString;

    public StoreRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }


    public IEnumerable<Store> GetAllStores()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("SELECT * FROM \"Stores\"", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        yield return new Store
                        {
                            Id = reader.GetInt32(0),
                            StoreName = reader.GetString(1)
                        };
                }
            }
        }
    }

    public Task<Store> GetStoreById(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("SELECT * FROM \"Stores\" WHERE \"Id\" = @id", connection))
            {
                command.Parameters.AddWithValue("id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return Task.FromResult(new Store
                        {
                            Id = reader.GetInt32(0),
                            StoreName = reader.GetString(1)
                        });
                }
            }
        }

        throw new StoreNotFoundException(id);
    }

    public Task<Store> CreateStore(Store store)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("INSERT INTO \"Stores\" (\"StoreName\") VALUES (@StoreName)", connection))
            {
                command.Parameters.AddWithValue("StoreName", store.StoreName);
                command.ExecuteNonQuery();
            }
        }

        return Task.FromResult(store);
    }

    public Task<Store> UpdateStore(int id, Store store)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("UPDATE \"Stores\" SET \"StoreName\" = @StoreName WHERE \"Id\" = @Id", connection))
            {
                command.Parameters.AddWithValue("StoreName", store.StoreName);
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();
            }
        }
        return Task.FromResult(store);
    }

    public Task DeleteStore(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("DELETE FROM \"Stores\" WHERE \"Id\" = @Id", connection))
            {
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();
            }
        }

        return Task.CompletedTask;
    }
}