using System.Text;
using System.Text.Json;
using Blazor.Services.Interfaces;
using Domain.DTOs;
using Domain.Models;

namespace Blazor.Services.Http;

public class ItemServiceImpl : IItemService
{
    private readonly HttpClient client = new HttpClient()
    {
        BaseAddress = new Uri("http://localhost:5193")
    };
    
    
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
    

    public async Task<Item> GetItemById(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"http://localhost:5193/Item/{id}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        
        Item item = JsonSerializer.Deserialize<Item>(content,new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return item;
    }

    public async Task<UpdateItemDto> UpdateItem(UpdateItemDto dto)
    {
        try
        {
            string itemAsJson = JsonSerializer.Serialize(dto);
            StringContent content = new(itemAsJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PatchAsync($"/Item/{dto.ItemId}", content);
            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to update item. Status Code: {response.StatusCode}, Error: {errorMessage}");
                throw new Exception(
                    $"Failed to update item. Status Code: {response.StatusCode}, Error: {errorMessage}");
            }

            string updatedItemAsJson = await response.Content.ReadAsStringAsync();
            UpdateItemDto updatedItem = JsonSerializer.Deserialize<UpdateItemDto>(updatedItemAsJson);
            return updatedItem;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in UpdateItem: {ex}");
            throw;
        }
    }

    public async Task DeleteItem(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"http://localhost:5193/Item/{id}");
                string responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(responseContent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        
        }
    }
