using System.Text;
using System.Text.Json;
using Blazor.Services.Interfaces;
using Domain.DTOs;

namespace Blazor.Services.Http;

public class WishListServiceImpl : IWishListService
{
    private readonly HttpClient client = new();

    public async Task AddToWishListAsync(AddToWishListDTO dto)
    {
        string orderJson = JsonSerializer.Serialize(dto);
        StringContent content = new(orderJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PostAsync("http://localhost:5193/WishList", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        // Check if the request was successful; if not, throw an exception
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
}