namespace Domain.Models;

public class Address
{
    public int DoorNumber { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public int PostalCode { get; set; }
    public string? Country { get; set; }
    public int id { get; }
    
    public Address(int doorNumber, string street, string city, string state, int postalCode, string country)
    {
        DoorNumber = doorNumber;
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }
    
    public Address()
    {
    }
}