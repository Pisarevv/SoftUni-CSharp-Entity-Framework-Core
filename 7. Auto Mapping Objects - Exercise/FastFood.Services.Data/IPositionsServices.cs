using FastFood.Core.ViewModels.Positions;

namespace FastFood.Services.Data;
public interface IPositionsServices
{
    public Task CreateAsync(CreatePositionInputModel inputModel);

    public Task<IEnumerable<PositionsAllViewModel>> GetAllPositionsAsync();
}
