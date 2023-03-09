using FastFood.Core.ViewModels.Positions;

namespace FastFood.Services.Data;
public interface IPositionsServices
{
    Task Create(CreatePositionInputModel inputModel);

    Task<IEnumerable<PositionsAllViewModel>> GetAllPositions();
}
