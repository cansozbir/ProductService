using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Abstractions.Products.Commands;
using ProductService.Application.Abstractions.Products.Queries;
using ProductService.Application.Abstractions.Stores.Queries;
using ProductService.Domain.Entities;

namespace ProductService.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns>List of all products.</returns>
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAllProducts()
    {
        var products = await _mediator.Send(new GetAllProducts());
        return Ok(products);
    }

    /// <summary>
    /// Get the product with the specified id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The product with the specified identifier, if it exists, otherwise it returns NotFound </returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById([FromRoute] int id)
    {
        try
        {
            var productById = await _mediator.Send(new GetProductById{Id = id});
            return Ok(productById);
        } 
        catch (ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
        
    }
    
    /// <summary>
    /// Creates a new product with specified name, cost and sales price, and returns the created product
    /// </summary>
    /// <param name="product"></param>
    /// <returns>Newly created product object, with the id it has alongside.</returns>
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        try
        {
            var productCreated = await _mediator.Send(new CreateProduct{ProductName = product.ProductName, Cost = product.Cost, SalesPrice = product.SalesPrice});
            return CreatedAtAction(nameof(GetProductById), new {id = productCreated.Id}, productCreated);
        }
        catch (ProductAlreadyExistsException e)
        {
            return Conflict(e.Message);
        }
    }

    /// <summary>
    /// Updates the product with the specified id, with the new values. 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="product"></param>
    /// <returns>The updated version of product if exists, otherwise returns NotFound.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
    {
        try
        {
            var updateProduct = await _mediator.Send(new UpdateProduct{Id = id, ProductName = product.ProductName, Cost = product.Cost, SalesPrice = product.SalesPrice});
            return Ok(updateProduct);
        }
        catch (ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
        
    }
    
    /// <summary>
    /// Deletes the product with the specified id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns Ok object no matter if the product was found and deleted, or not.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _mediator.Send(new DeleteProduct{Id = id});
        return Ok();
    }
    
    /// <summary>
    /// Gets the best seller product by quantity.
    /// </summary>
    /// <returns>The best seller product by quantity.</returns>
    [HttpGet("bestSellerByQuantity")]
    public async Task<ActionResult<Product>> GetBestSellerByQuantity()
    {
        try
        {
            var bestSellerProductByQuantity = await _mediator.Send(new GetBestSellerProductByQuantity{});
            return Ok(bestSellerProductByQuantity);
        } 
        catch (ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}

