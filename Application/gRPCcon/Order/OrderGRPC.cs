using Application.Logic;
using Application.LogicInterfaces;
using Grpc.Net.Client;

namespace Application.gRPCcon.Order;

public class OrderGRPC: IOrderGRPC
{
    public async Task<Domain.Models.Order> CreateAsync(Domain.Models.Order order)
    {
        // Establish a gRPC channel to the server at the specified address
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        // Create a client for the OrderService using the gRPC channel
        var client = new OrderService.OrderServiceClient(channel);
        
        // Convert the domain model Order to the gRPC OrderP
        OrderP orderP = ConvertToOrderP(order);
        
        // Prepare a gRPC request to create an order
        OrderPRequest orderToCreate = new OrderPRequest()
        {
            Customer = orderP.Customer,
            Items = {orderP.Items}, // Assuming Items is a repeated field
            Oder = orderP // its oder but if make it order the rebuild doesnt work !?!? 
        };
        
        // Call the gRPC service method asynchronously to create the order
        var response = await client.createOrderAsync(orderToCreate);
        
        // Shutdown the gRPC channel once the operation is completed
        channel.ShutdownAsync();
        
        // Update the domain model Order with the returned order ID
        order.Id = response.Order.Id; 
        
        // Return a completed task with the original order as a result
        return Task.FromResult(order).Result;
    }

    private OrderP ConvertToOrderP(Domain.Models.Order order)
    {
        // Convert the customer information from domain model to gRPC model
        CustomerP customer = ConvertToCustomerP(order.Customer);
        // Convert the address information from domain model to gRPC model
        AddressP address = ConvertToAddressP(order.Address);
        // Convert the list of items from domain model to a list of gRPC items
        List<ItemP> itemsP = ConvertToItemPList(order.Items);

        // Create and return a new gRPC OrderP object with the converted information
        return new OrderP
        {
            Id = order.Id,
            Customer = customer,
            Items = { itemsP },
            Address = address,
            OrderDate = order.OrderDate,
            DeliveryDate = order.DeliveryDate,
            IsConfirmed = order.IsConfirmed,
            TotalPrice = order.TotalPrice
        };
    }
    
    private CustomerP ConvertToCustomerP(Domain.Models.Customer customer)
    {
        // Create a new gRPC AddressP object and populate it with address information from the domain model
        AddressP address = new AddressP
        {
            DoorNumber = customer.Address.DoorNumber,
            Street = customer.Address.Street,
            City = customer.Address.City,
            State = customer.Address.State,
            PostalCode = customer.Address.PostalCode,
            Country = customer.Address.Country
        };
        // Create and return a new gRPC CustomerP object with the converted information
        return new CustomerP
        {
            Username = customer.UserName,
            Password = customer.Password,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Address = address,
            Role = customer.Role
        };
    }

    private AddressP ConvertToAddressP(Domain.Models.Address address)
    {
        // Create and return a new gRPC AddressP object with the converted information
        return new AddressP
        {
            DoorNumber = address.DoorNumber,
            Street = address.Street,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Country = address.Country
        };
    }

    private List<ItemP> ConvertToItemPList(List<Domain.Models.Item> items)
    {
        // Create a new list to store the converted gRPC ItemP objects
        List<ItemP> itemsP = new List<ItemP>();
        
        // Iterate through each item in the input list and convert it to a gRPC ItemP object
        foreach (Domain.Models.Item item in items)
        {
            // Create a new gRPC ItemP object and populate it with information from the domain model item
            ItemP itemP = new ItemP
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Price = item.Price,
                Category = item.Category,
                Stock = item.Stock,
                Description = item.Description
            };
            // Add the converted item to the list
            itemsP.Add(itemP);
        }
        // Return the list of converted gRPC ItemP objects
        return itemsP;
    }
}