using System.ComponentModel.DataAnnotations;
using Application.gRPCcon.Costumer;
using Domain.Models;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly ICostumerGrpc iCostumerGrpc;
    

    public AuthService(ICostumerGrpc IcostumerGrpc)
    {
        this.iCostumerGrpc = iCostumerGrpc;
    }
    
    public Task<Costumer> ValidateUser(string username, string password)
    {

        Task<Costumer?> existingUser = iCostumerGrpc.GetUserAsync(username, password);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Result.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }
    

    public Task RegisterUser(Costumer costumer)
    {

        if (string.IsNullOrEmpty(costumer.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(costumer.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        return Task.CompletedTask;
    }
}