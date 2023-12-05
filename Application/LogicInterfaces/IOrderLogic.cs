using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IOrderLogic
{
    Task CreateAsync(CreateOrderDto dto);
    Task ConfirmAsync(Order order);
}