namespace Domain.DTOs;

public class CustomerLoginDto
{
    public string Username { get; init; }
    public string Password { get; init; }
    
    public CustomerLoginDto(string username, string password)
    {
        this.Username = username.ToString();
        Password = password;
    }

    public CustomerLoginDto()
    {
        
    }
}