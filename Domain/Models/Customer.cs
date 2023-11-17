using System.Globalization;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class Customer
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Address Address { get; set; } = new Address();

}