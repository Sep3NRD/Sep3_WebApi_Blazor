using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class CustomerController: ControllerBase
{
    private readonly ICustomerLogic customerLogic;

    public CustomerController(ICustomerLogic customerLogic)
    {
        this.customerLogic = customerLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Customer>> CreateAsync(Customer customer)
    {
        Console.WriteLine(customer.UserName);
        try
        {
            Customer customerToCreate = await customerLogic.CreateAsync(customer);
            return Created($"/customers/{customer.Id}", customerToCreate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<Customer>> GetAsync([FromQuery] string username, string password)
    {
        try
        {
            CustomerLoginDto loginDto = new(username,password);
            Customer customer = await customerLogic.LoginValidation(loginDto);
            return Ok(customer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}