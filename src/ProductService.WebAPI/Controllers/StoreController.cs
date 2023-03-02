using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Abstractions.Stores.Commands;
using ProductService.Application.Abstractions.Stores.Queries;
using ProductService.Domain.Entities;

namespace ProductService.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoreController : ControllerBase
{
    private readonly IMediator _mediator;

    public StoreController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Gets all stores.
    /// </summary>
    /// <returns>A list of all stores</returns>
    [HttpGet]
    public async Task<ActionResult<List<Store>>> GetAllStores()
    {
        var stores = await _mediator.Send(new GetAllStores());
        return Ok(stores);
    }
    
    /// <summary>
    /// Gets the store with the specified id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The store with the specified identifier, if it exists, otherwise it returns NotFound.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Store>> GetStoreById([FromRoute] int id)
    {
        try
        {
            var storeById = await _mediator.Send(new GetStoreById{StoreId = id});
            return Ok(storeById);
        }
        catch (StoreNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Creates a new store with specified name, and returns the created store 
    /// </summary>
    /// <param name="store"></param>
    /// <returns>Id of the newly created store, with the store object alongside. </returns>
    [HttpPost]
    public async Task<ActionResult<Store>> CreateStore(Store store)
    {
        var storeCreated = await _mediator.Send(new CreateStore{StoreName = store.StoreName});
        return CreatedAtAction(nameof(GetStoreById), new {id = storeCreated.Id}, storeCreated);
    }

    /// <summary>
    /// Updates the store with the specified id and returns the updated store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="store"></param>
    /// <returns>The updated version of store if exists, otherwise returns NotFound</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Store>> UpdateStore(int id, Store store)
    {
        try
        {
            var updateStore = await _mediator.Send(new UpdateStore{StoreId = id, StoreName = store.StoreName});
            return Ok(updateStore);
        } 
        catch (StoreNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    /// <summary>
    /// Deletes the store with the specified id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns Ok object no matter if the store was found and deleted, or not.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStore(int id)
    {
        await _mediator.Send(new DeleteStore{StoreId = id});
        return Ok();
    }
    
    
    /// <summary>
    /// Gets the profit of the store with the specified id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Profit in integer. Returns NotFound if there was no Store, Sale, or Product found. </returns>
    [HttpGet("{id}/profit")]
    public async Task<ActionResult<int>> GetStoreProfit(int id)
    {
        try
        {
            var storeProfit = await _mediator.Send(new GetStoreProfit{StoreId = id});
            return Ok(storeProfit);
        }
        catch (StoreNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (SaleNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    /// <summary>
    /// Gets the most profitable store.
    /// Calculated with (SalesPrice - Cost) * SalesQuantity.
    /// Stock information didn't considered.
    /// </summary>
    /// <returns>Store with the biggest profit. Returns NotFound if no Store, Product, or Sale found. </returns>
    [HttpGet("mostProfitable")]
    public async Task<ActionResult<Store>> GetMostProfitableStore()
    {
        try
        {
            var mostProfitableStore = await _mediator.Send(new GetStoreMostProfitable());
            return Ok(mostProfitableStore);
        }
        catch (StoreNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (SaleNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}
