using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ICostumerLogic
{
    Task<Costumer> CreateAsync(UserCreationDto userToCreate);
    public Task<IEnumerable<Costumer>> GetAsync(SearchUserParametersDto searchParameters);
}