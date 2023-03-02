using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Abstractions.InventorySales.Commands;
using ProductService.Application.Abstractions.InventorySales.Queries;
using ProductService.Domain.Entities;

namespace ProductService.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventorySaleController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventorySaleController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Gets all InventorySales.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<InventorySale>>> GetAllInventorySales()
    {
        var inventorySales = await _mediator.Send(new GetAllInventorySales());
        return Ok(inventorySales);
    }

    /// <summary>
    /// Gets the InventorySale with the specified id if it exists.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>InventorySale with the specified id if exists, otherwise NotFound.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<InventorySale>> GetInventorySaleById([FromRoute] int id)
    {
        try
        {
            var inventorySaleById = await _mediator.Send(new GetInventorySaleById{Id = id});
            return Ok(inventorySaleById);
        } 
        catch (SaleNotFoundException e)
        {
            return NotFound(e.Message);
        }
        
    }

    /// <summary>
    /// Creates a new InventorySale with the specified product id, store id, sales quantity, date and stock, and returns the created InventorySale.
    /// </summary>
    /// <param name="inventorySale"></param>
    /// <returns>Newly created InventorySale object, with the id it has alongside.</returns>
    [HttpPost]
    public async Task<ActionResult<InventorySale>> CreateInventorySale(InventorySale inventorySale)
    {
        var inventorySaleCreated = await _mediator.Send(new CreateInventorySale{ProductId = inventorySale.ProductId, StoreId = inventorySale.StoreId, SalesQuantity = inventorySale.SalesQuantity, Date = inventorySale.Date, Stock = inventorySale.Stock});
        return CreatedAtAction(nameof(GetInventorySaleById), new {id = inventorySaleCreated.Id}, inventorySaleCreated);
    }

    /// <summary>
    /// Updates the InventorySale with the specified id, with the specified product id, store id, sales quantity, date and stock.
    /// Doesn't update the id. It only updates the product id, store id, sales quantity, date and stock.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="inventorySale"></param>
    /// <returns>Updated InventorySale object if exists, otherwise NotFound.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<InventorySale>> UpdateInventorySale(int id, InventorySale inventorySale)
    {
        try
        {
            var updateInventorySale = await _mediator.Send(new UpdateInventorySale{Id = id, ProductId = inventorySale.ProductId, StoreId = inventorySale.StoreId, SalesQuantity = inventorySale.SalesQuantity, Date = inventorySale.Date, Stock = inventorySale.Stock});
            return Ok(updateInventorySale);
        }
        catch (SaleNotFoundException e)
        {
            return NotFound(e.Message);
        }
        
    }
    
    /// <summary>
    /// Deletes the InventorySale with the specified id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Ok no matter if the InventorySale exists and deleted, or not.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInventorySale([FromRoute] int id)
    {
        await _mediator.Send(new DeleteInventorySale{Id = id});
        return Ok();
    }
    
    /// <summary>
    /// Gets all InventorySales with the specified storeId.
    /// </summary>
    /// <param name="storeId"></param>
    /// <returns>List of InventorySales with the specified storeId.</returns>
    [HttpGet("store/{storeId}")]
    public async Task<ActionResult<List<InventorySale>>> GetInventorySalesByStoreId([FromRoute] int storeId)
    {
        var inventorySalesByStoreId = await _mediator.Send(new GetInventorySalesByStoreId{StoreId = storeId});
        return Ok(inventorySalesByStoreId);
    }
}

