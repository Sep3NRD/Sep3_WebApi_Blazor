using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerLogic customerLogic;

    // Constructor to inject ICustomerLogic dependency
    public CustomerController(ICustomerLogic customerLogic)
    {
        this.customerLogic = customerLogic;
    }

    // Action method to create a new customer
    [HttpPost]
    public async Task<ActionResult<Customer>> CreateAsync(Customer customer)
    {
        try
        {
            // Call the business logic to create a new customer
            Customer customerToCreate = await customerLogic.CreateAsync(customer);

            // Return a 201 Created response with the created customer
            return Created($"/customer/{customer.Id}", customerToCreate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);  // Log the exception
            // Return a 500 Internal Server Error response with the exception message
            return StatusCode(500, e.Message);
        }
    }

    // Action method to get a customer by username
    [HttpGet]
    public async Task<ActionResult<Customer>> GetAsync([FromQuery] string username)
    {
        try
        {
            // Create a login DTO with the provided username
            CustomerLoginDto loginDto = new(username, "");

            // Call the business logic to get a customer by username
            var customer = await customerLogic.GetByUsernameAsync(loginDto);

            // Return a 200 OK response with the retrieved customer
            return Ok(customer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);  // Log the exception
            // Return a 500 Internal Server Error response with the exception message
            return StatusCode(500, e.Message);
        }
    }

    // Action method to update customer information
    [HttpPatch("update")]
    public async Task<ActionResult> UpdateAsync(Customer customer)
    {
        try
        {
            // Call the business logic to update customer information
            await customerLogic.UpdateAsync(customer);

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
    
    [HttpPatch("addNewAddress")]
    public async Task<ActionResult> AddNewAddress(AddNewAddressDTO dto)
    {
        try
        {
            // Call the business logic to update customer information
            await customerLogic.AddNewAddress(dto);

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
    
    [HttpGet("{username}")]

    public async Task<ActionResult<ICollection<Address>>> GetByIdAsync([FromRoute] string username)
    {
        try
        {
            var addresses = await customerLogic.GetAddressesByUsername(username);
            return Created("/Customer", addresses);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}