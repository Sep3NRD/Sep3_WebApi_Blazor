using Application.Logic;
using Application.LogicInterfaces;
using Grpc.Net.Client;

namespace Application.gRPCcon.Order;

public class OrderGRPC: IOrderGRPC
{
    public async Task<Domain.Models.Order> CreateAsync(Domain.Models.Order order)
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new OrderService.OrderServiceClient(channel);
        
        OrderP orderP = ConvertToOrderP(order);
        
        OrderPRequest orderToCreate = new OrderPRequest()
        {
            Customer = orderP.Customer,
            Items = {orderP.Items},
            Oder = orderP // its oder but if make it order the rebuild doesnt work !?!?
        };

        var response = await client.createOrderAsync(orderToCreate);
        channel.ShutdownAsync();
        order.Id = response.Order.Id;
        return Task.FromResult(order).Result;
    }

    private OrderP ConvertToOrderP(Domain.Models.Order order)
    {
        CustomerP customer = ConvertToCustomerP(order.Customer);
        AddressP address = ConvertToAddressP(order.Address);
        List<ItemP> itemsP = ConvertToItemPList(order.Items);

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
        AddressP address = new AddressP
        {
            DoorNumber = customer.Address.DoorNumber,
            Street = customer.Address.Street,
            City = customer.Address.City,
            State = customer.Address.State,
            PostalCode = customer.Address.PostalCode,
            Country = customer.Address.Country
        };

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
        List<ItemP> itemsP = new List<ItemP>();

        foreach (Domain.Models.Item item in items)
        {
            ItemP itemP = new ItemP
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Price = item.Price,
                Category = item.Category,
                Stock = item.Stock,
                Description = item.Description
            };

            itemsP.Add(itemP);
        }

        return itemsP;
    }
}