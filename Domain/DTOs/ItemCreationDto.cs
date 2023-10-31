namespace Domain.DTOs;

public class ItemCreationDto
{
    public string Name { get; set; }

    public ItemCreationDto(string name)
    {
        Name = name;
    }
}