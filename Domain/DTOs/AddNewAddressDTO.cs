namespace Domain.DTOs;

public class AddNewAddressDTO
{
    public int DoorNumber { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public int PostalCode { get; set; }
    public string? Country { get; set; }
    public string? username { get; set; }
}