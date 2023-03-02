using Application.Exceptions;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ProductService.Application.Abstractions;
using ProductService.Domain.Entities;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly string? _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }


    public IEnumerable<Product> GetAllProducts()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("SELECT * FROM \"Products\"", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        yield return new Product
                        {
                            Id = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            Cost = reader.GetInt32(2),
                            SalesPrice = reader.GetInt32(3)
                        };
                }
            }
        }
    }

    public Task<Product> GetProductById(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("SELECT * FROM \"Products\" WHERE \"Id\" = @id", connection))
            {
                command.Parameters.AddWithValue("id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return Task.FromResult(new Product
                        {
                            Id = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            Cost = (int) reader.GetDecimal(2),
                            SalesPrice = (int) reader.GetDecimal(3)
                        });
                }
            }
        }

        throw new ProductNotFoundException(id);
    }

    public Task<Product> CreateProduct(Product product)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand(
                       "INSERT INTO \"Products\" (\"ProductName\", \"SalesPrice\", \"Cost\") VALUES (@productName, @salesPrice, @cost) RETURNING \"Id\"",
                       connection))
            {
                command.Parameters.AddWithValue("productName", product.ProductName);
                command.Parameters.AddWithValue("salesPrice", product.SalesPrice);
                command.Parameters.AddWithValue("cost", product.Cost);
                product.Id = (int) command.ExecuteScalar();
            }
        }
        return Task.FromResult(product);
    }

    public Task<Product> UpdateProduct(int id, Product product)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        { 
            connection.Open();
            using (var command = new NpgsqlCommand(
                "UPDATE \"Products\" SET \"ProductName\" = @productName, \"SalesPrice\" = @salesPrice, \"Cost\" = @cost WHERE \"Id\" = @id",
                connection))
            {
                command.Parameters.AddWithValue("productName", product.ProductName);
                command.Parameters.AddWithValue("salesPrice", product.SalesPrice);
                command.Parameters.AddWithValue("cost", product.Cost);
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
            }
        }

        return Task.FromResult(product);
    }

    public Task DeleteProduct(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("DELETE FROM \"Products\" WHERE \"Id\" = @Id", connection))
            {
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();
            }
        }

        return Task.CompletedTask;
    }
    
    public IEnumerable<Product> GetProductsByIds(int[] productIds)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand("SELECT * FROM \"Products\" WHERE \"Id\" = ANY(@productIds)", connection))
            {
                command.Parameters.AddWithValue("productIds", productIds);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        yield return new Product
                        {
                            Id = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            Cost = (int) reader.GetDecimal(2),
                            SalesPrice = (int) reader.GetDecimal(3)
                        };
                }
            }
        }
    }
}