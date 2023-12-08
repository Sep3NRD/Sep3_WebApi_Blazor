using Application.Logic;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route(("[controller]"))]
public class WishListController: ControllerBase
{
    private readonly IWishListLogic wishListLogic;

    public WishListController(IWishListLogic wishListLogic)
    {
        this.wishListLogic = wishListLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<WishList>> AddToWishListAsync(AddToWishListDTO dto)
    {
        try
        {
            await wishListLogic.AddToWishListAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet ("{username}")]
    public async Task<ActionResult<WishList>> GetWishList(string username)
    {
        try
        {
            var result =await wishListLogic.GetWishListAsync(username);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}