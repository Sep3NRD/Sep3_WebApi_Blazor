using Domain.Models;

namespace Application.LogicInterfaces;

public interface IOrderLogic
{
    Task<Order> CreateAsync(Order order);
    Task ConfirmAsync(Order order);
}