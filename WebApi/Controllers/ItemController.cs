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
        this.ItemLogic = itemLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Item>> CreateAsync([FromBody] ItemCreationDto dto)
    {
        try
        {
            Item created = await ItemLogic.CreateAsync(dto);
            return Created($"/items/{created.ItemId}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine();
            return StatusCode(500, e.Message);
        }
    }
}
