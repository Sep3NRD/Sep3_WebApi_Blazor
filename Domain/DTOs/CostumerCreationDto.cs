namespace Domain.DTOs;

public class CostumerCreationDto
{
    public string UserName { get;}
    public string Password { get; }
    public string firstName { get; }
    public string lastName { get; }

    public CostumerCreationDto(string userName,string password)
    {
        UserName = userName;
        Password = password;
    }
}