using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Domain.Models;
using FileData.DAOs;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserDao iUserDao;
    

    public AuthService(IUserDao iUserDao)
    {
        this.iUserDao = iUserDao;
    }
    
    public Task<Costumer> ValidateUser(string username, string password)
    {

        Task<Costumer?> existingUser = iUserDao.GetUserAsync(username, password);
        
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