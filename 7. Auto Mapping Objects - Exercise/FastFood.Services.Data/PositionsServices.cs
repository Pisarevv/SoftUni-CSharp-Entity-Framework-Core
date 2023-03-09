
namespace FastFood.Services.Data;

using AutoMapper;
using FastFood.Core.ViewModels.Positions;
using FastFood.Data;
using FastFood.Models;

public class PositionsServices : IPositionsServices
{
    //Dependency Injection
    private readonly IMapper mapper;
    private readonly FastFoodContext context; 
      
    public PositionsServices(IMapper mapper, FastFoodContext context)
    {
        this.mapper = mapper;
        this.context = context;
    }

    public async Task CreateAsync(CreatePositionInputModel inputModel)
    {
        Position position = this.mapper.Map<Position>(inputModel);

        await context.Positions.AddAsync(position);
    }

    Task<IEnumerable<PositionsAllViewModel>> IPositionsServices.GetAllPositionsAsync()
    {
        throw new NotImplementedException();
    }
}
