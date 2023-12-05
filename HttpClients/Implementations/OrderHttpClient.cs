using System.Net.Http.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class OrderHttpClient: IOrderService
{
    private readonly HttpClient client;

    public OrderHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(OrderCreationDto dto)
    {
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync("/orders", dto);
        string result = await responseMessage.Content.ReadAsStringAsync();    
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }

    public async Task ConfirmAsync(Order order)
    {
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync("/orders", order);
        string result = await responseMessage.Content.ReadAsStringAsync();    
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }
}