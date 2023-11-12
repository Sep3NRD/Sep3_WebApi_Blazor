using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class CostumerLogic : ICostumerLogic
{
    public CostumerLogic()
    {
        
    }
    public Task<Costumer> CreateAsync(UserCreationDto userToCreate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Costumer>> GetAsync(SearchUserParametersDto searchParameters)
    {
        throw new NotImplementedException();
    }
}