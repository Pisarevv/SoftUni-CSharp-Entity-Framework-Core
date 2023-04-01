using Boardgames.Data.Common;
using Boardgames.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ImportDto;

[XmlType("Boardgame")]
public class ImportBoardgameDto
{
    [XmlElement("Name")]
    [MaxLength(ValidationConstants.MaxBoardgameNameLength)]
    [MinLength(ValidationConstants.MinBoardgameNameLength)]
    public string Name { get; set; } = null!;

    [XmlElement("Rating")]
    [Range(ValidationConstants.MinBoardgameRating,ValidationConstants.MaxBoardgameRating)]
    public double Rating { get; set; }

    [XmlElement("YearPublished")]
    [Range(ValidationConstants.MinYearPublishedBoardgame , ValidationConstants.MaxYearPublishedBoardgame)]
    public int YearPublished { get; set; }


    [XmlElement("CategoryType")]
    [Range(0,4)]
    public int CategoryType { get; set; } 

    [XmlElement("Mechanics")]
    public string Mechanics { get; set; } = null!;
}
