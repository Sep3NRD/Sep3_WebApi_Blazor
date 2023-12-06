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

    
   
}