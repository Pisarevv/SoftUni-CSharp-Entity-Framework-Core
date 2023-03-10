
namespace FastFood.Services.Data;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastFood.Core.ViewModels.Positions;
using FastFood.Data;
using FastFood.Models;
using Microsoft.EntityFrameworkCore;

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

        await this.context.Positions.AddAsync(position);
        await this.context.SaveChangesAsync();
    }

    public async Task<IEnumerable<PositionsAllViewModel>> GetAllPositionsAsync()
    => await this.context.Positions
             .ProjectTo<PositionsAllViewModel>(this.mapper.ConfigurationProvider)
             .ToArrayAsync();
}
