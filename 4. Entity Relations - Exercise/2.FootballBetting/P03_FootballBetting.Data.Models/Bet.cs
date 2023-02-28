namespace P03_FootballBetting.Data.Models;
using System.ComponentModel.DataAnnotations;
using Common;
public class Bet
{
    [Key]
    public int BetId { get; set; }

    public decimal Amount { get; set; }

    [MaxLength(ValidationConstants.MaxPredictionLength)]
    public string Prediction { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public int UserId { get; set; }

    public int GameId { get; set; }

    //TODO: Add relations
}
