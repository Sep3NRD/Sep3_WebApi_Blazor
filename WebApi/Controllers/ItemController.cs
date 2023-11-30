﻿using Application.Logic;
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
    public async Task<ActionResult<Item>> CreateAsync([FromBody] Item item)
    {
        try
        {
            Item created = await ItemLogic.CreateAsync(item);
            return Created($"/items/{created.Name}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine();
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetAsync([FromQuery] string? Name, [FromQuery] double Price)
    {
        try
        {
            SearchItemParametersDto parameters = new(Name, Price);
            var items = await ItemLogic.GetAsync().ConfigureAwait(false); // nuri-->  not using filters for now
            //return Ok(items);
            return Created($"/items", items);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


    [HttpGet("{id}")]

public async Task<ActionResult<Item>> GetByIdAsync(int id)
    {
        try
        {
            var item = await ItemLogic.GetByIdAsync(id);
            return item;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }



    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        Console.WriteLine(id);
        try
        {
            await ItemLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch("{ItemId}")]
    public async Task<ActionResult<UpdateItemDto>> UpdateAsync(int itemId, double price, int stock)
    {
        try
        {
            UpdateItemDto updated = new UpdateItemDto(itemId, price, stock);
            var items = await ItemLogic.UpdateItemAsync(updated);
            
            return Ok(items);
        }
        catch (Exception e)
        {
            Console.WriteLine();
            return StatusCode(500, e.Message);
        }
    }
}

