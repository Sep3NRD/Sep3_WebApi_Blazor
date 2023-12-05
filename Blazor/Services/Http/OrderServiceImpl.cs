using System.Text;
using System.Text.Json;
using Blazor.Services.Interfaces;
using Domain.DTOs;
using Domain.Models;

namespace Blazor.Services.Http;

public class OrderServiceImpl: IOrderService
{
    private readonly HttpClient client = new();
    
    public async Task CreateAsync(CreateOrderDto dto)
    {
        string orderJson = JsonSerializer.Serialize(dto);
        StringContent content = new(orderJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PostAsync("http://localhost:5193/Order", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        // Check if the request was successful; if not, throw an exception
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
}