using System.Net.Http.Json;
using Domain.DTOs;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class ItemHttpClient : IItemService
{
    private readonly HttpClient client;
    
    public ItemHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task CreateAsync(ItemCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/items", dto);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            
            throw new Exception(content);
        }
    }
}