using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]

public class ItemController : ControllerBase
{
    private readonly IItemLogic ItemLogic;

    public ItemController(IItemLogic itemLogic)
    {
      ItemLogic = itemLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Item>> CreateAsync([FromBody] ItemCreationDto dto)
    {
        
        try
        {
            Item created = await ItemLogic.CreateAsync(dto);
            return Created($"/items/{created.Name}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine();
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetAsync([FromQuery] string? Name, [FromQuery] double? Price)
    {
        try
        {
            SearchItemParametersDto parameters = new(Name, Price);
            var items = await ItemLogic.GetAsync(parameters);
            return Ok(items);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}
