﻿using Application.LogicInterfaces;
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
        try
        {
            Customer customerToCreate = await customerLogic.CreateAsync(customer);
            return Created($"/customer/{customer.Id}", customerToCreate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<Customer>> GetAsync([FromQuery] string username)
    {
        try
        {
            CustomerLoginDto loginDto = new(username,"");
            var customer = await customerLogic.GetByUsernameAsync(loginDto);
            return Ok(customer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateAsync(Customer customer)
    {
        try
        {
            await customerLogic.UpdateAsync(customer);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}