using FastFood.Common;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Core.ViewModels.Positions
{
    public class CreatePositionInputModel
    {
        [StringLength(ValidationConstants.PositionNameMaxLenghth, MinimumLength = 3)]
        public string PositionName { get; set; } = null!;
    }
}