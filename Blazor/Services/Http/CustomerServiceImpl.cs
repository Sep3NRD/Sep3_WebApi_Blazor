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

        HttpResponseMessage response = await client.PostAsync("https://localhost:9090/customers", content);
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

    public Task<Customer> GetByUsernameAsync(CustomerLoginDto userLoginDto)
    {
        throw new NotImplementedException();
    }
}