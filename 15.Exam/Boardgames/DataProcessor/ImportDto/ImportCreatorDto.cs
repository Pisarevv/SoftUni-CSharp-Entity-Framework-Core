using Boardgames.Data.Common;
using Boardgames.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ImportDto;

[XmlType("Creator")]
public class ImportCreatorDto
{
    [XmlElement("FirstName")]
    [MaxLength(ValidationConstants.MaxCreatorFirstNameLength)]
    [MinLength(ValidationConstants.MinCreatorFirstNameLength)]
    public string FirstName { get; set; } = null!;

    [XmlElement("LastName")]
    [MaxLength(ValidationConstants.MaxCreatorLastNameLength)]
    [MinLength(ValidationConstants.MinCreatorLastNameLength)]
    public string LastName { get; set; } = null!;

    [XmlArray("Boardgames")]
    public ImportBoardgameDto[] Boardgames { get; set; }

}
