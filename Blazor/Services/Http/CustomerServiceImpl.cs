using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;

namespace Blazor.Services.Http;

public class CustomerServiceImpl:ICustomerService
{
    // HttpClient for making HTTP requests
    private readonly HttpClient client = new();

    // Create a new customer asynchronously
    public async Task CreateAsync(Customer customer)
    {
        // Convert customer object to JSON
        string customerAsJson = JsonSerializer.Serialize(customer);
        StringContent content = new(customerAsJson, Encoding.UTF8, "application/json");

        // Send a POST request to create a new customer
        HttpResponseMessage response = await client.PostAsync("http://localhost:5193/Customer", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        // Check if the request was successful; if not, throw an exception
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    // Get a customer asynchronously based on user login information (not implemented)
    public Task<Customer> GetAsync(CustomerLoginDto userLoginDto)
    {
        throw new NotImplementedException();
    }

    // Get a customer by username asynchronously
    public async Task<Customer> GetByUsernameAsync(CustomerLoginDto userLoginDto)
    {
        string username = userLoginDto.Username;

        // Send a GET request to retrieve customer information by username
        HttpResponseMessage response = await client.GetAsync($"http://localhost:5193/Customer?username={username}");
        string responseContent = await response.Content.ReadAsStringAsync();

        // Check if the request was successful; if not, throw an exception
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        // Deserialize the JSON response to a Customer object
        var customer = JsonSerializer.Deserialize<Customer>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return customer;
    }

    // Update customer information asynchronously
    public async Task UpdateCustomerAsync(Customer customer)
    {
        // Convert customer object to JSON
        string customerAsJson = JsonSerializer.Serialize(customer);
        StringContent content = new(customerAsJson, Encoding.UTF8, "application/json");

        // Send a PATCH request to update customer information
        HttpResponseMessage response = await client.PatchAsync("http://localhost:5193/Customer", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        // Check if the request was successful; if not, throw an exception
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
}
