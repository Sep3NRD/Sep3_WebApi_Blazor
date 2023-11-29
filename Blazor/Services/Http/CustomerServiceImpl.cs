using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;

namespace Blazor.Services.Http;

public class CustomerServiceImpl:ICustomerService
{
    private readonly HttpClient client = new();
    
    public async Task CreateAsync(Customer customer)
    {
        string customerAsJson = JsonSerializer.Serialize(customer);
        StringContent content = new(customerAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("http://localhost:5193/Customer", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public Task<Customer> GetAsync(CustomerLoginDto userLoginDto)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer> GetByUsernameAsync(CustomerLoginDto userLoginDto)
    {
        string username = userLoginDto.Username;
        HttpResponseMessage response = await client.GetAsync($"http://localhost:5193/Customer?username={username}");

        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        var customer = JsonSerializer.Deserialize<Customer>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return customer;

    }
}