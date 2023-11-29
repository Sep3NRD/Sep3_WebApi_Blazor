using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
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
        
        HttpResponseMessage response = await client.PostAsJsonAsync("/item", dto);
        
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Item>> GetAsync(string? Name, double? Price)
    {
        HttpResponseMessage response = await client.GetAsync("/items");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Item> items = JsonSerializer.Deserialize<ICollection<Item>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return items;
    }

    public async Task<Item> GetItemById(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/items/{id}");
        string content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            Item item = JsonSerializer.Deserialize<Item>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return item;
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        else
        {
            throw new Exception(
                $"Failed to retrieve item with ID {id}. Status code: {response.StatusCode}. Content: {content}");
        }
    }
}