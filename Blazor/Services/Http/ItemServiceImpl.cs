using System.Text;
using System.Text.Json;
using Blazor.Services.Interfaces;
using Domain.Models;

namespace Blazor.Services.Http;

public class ItemServiceImpl : IItemService
{
    private readonly HttpClient client = new();
    
    
    public async Task CreateAsync(Item item)
    {
        string itemAsJson = JsonSerializer.Serialize(item);
        StringContent content = new(itemAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("http://localhost:5193/Item", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public async Task<ICollection<Item>> GetAsync()
    {
        HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:5193/Item");
        string content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Item> items = JsonSerializer.Deserialize<ICollection<Item>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return items;
    }
}