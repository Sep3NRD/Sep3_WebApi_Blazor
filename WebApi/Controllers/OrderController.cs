using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


[ApiController]
[Route("[controller]")]
public class OrderController: ControllerBase
{
    private readonly IOrderLogic orderLogic;

    public OrderController(IOrderLogic orderLogic)
    {
        this.orderLogic = orderLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateAsync(CreateOrderDto dto)
    {
        
        try
        {
            await orderLogic.CreateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetAsync()
    {
        try
        {
            var orders = await orderLogic.GetAllAsync();
            return Ok(orders);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch("Confirm/{orderId}")]
    public async Task<ActionResult> ConfirmAsync([FromRoute] int orderId)
    {
        try
        {
            // Call the business logic to update customer information
            await orderLogic.ConfirmAsync(orderId);

            // Return a 200 OK response
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);  // Log the exception
            // Return a 500 Internal Server Error response with the exception message
            return StatusCode(500, e.Message);
        }
    }
    
    
}