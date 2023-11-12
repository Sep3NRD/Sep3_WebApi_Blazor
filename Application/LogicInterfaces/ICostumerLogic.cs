using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ICostumerLogic
{
    Task<Costumer> CreateAsync(UserCreationDto userToCreate);
    Task<IEnumerable<Costumer>> GetAsync(SearchUserParametersDto searchParameters);
    
}