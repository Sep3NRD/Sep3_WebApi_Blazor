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
    
    public Task<Customer> ValidateUser(string username, string password)
    {

        // Task<Costumer?> existingUser = iCostumerGrpc.GetUserAsync(username, password);
        //
        // if (existingUser == null)
        // {
        //     throw new Exception("User not found");
        // }
        //
        // if (!existingUser.Result.Password.Equals(password))
        // {
        //     throw new Exception("Password mismatch");
        // }
        //
        // return existingUser;

        return null;
    }
    

    public Task RegisterUser(Customer customer)
    {

        if (string.IsNullOrEmpty(customer.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(customer.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        return Task.CompletedTask;
    }
}