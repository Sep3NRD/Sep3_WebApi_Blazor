using System.Security.Claims;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.LogicInterfaces;
using Domain.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly ICustomerLogic  iCustomerLogic;

    public AuthController(IConfiguration config, ICustomerLogic iCustomerLogic)
    {
        this.config = config;
        this.iCustomerLogic = iCustomerLogic;
    }
    
    private List<Claim> GenerateClaims(Customer customer)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, customer.UserName),
            new Claim("Id",customer.Id.ToString()),
        };
        return claims.ToList();
    }
    
    private string GenerateJwt(Customer customer)
    {
        List<Claim> claims = GenerateClaims(customer);
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims, 
            null,
            DateTime.UtcNow.AddMinutes(60));
    
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        
        try
        {
            Customer customer = await iCustomerLogic.GetAsync(userLoginDto);
            string token = GenerateJwt(customer);
    
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest("User not found");
        }
    }
    
    
}