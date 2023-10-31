using System.Net.Http.Json;
using Domain.DTOs;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class ItemHttpClass : IItemService
{
    private readonly HttpClient client;
    
    public async Task CreateAsync(ItemCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/items", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}