namespace P03_FootballBetting.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common;
public class Bet
{
    [Key]
    public int BetId { get; set; }

    public decimal Amount { get; set; }

    [MaxLength(ValidationConstants.MaxPredictionLength)]
    public string Prediction { get; set; } = null!;

    public DateTime DateTime { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public User User { get; set; } = null!;

    [ForeignKey(nameof(GameId))]
    public int GameId { get; set; }

    public Game Game { get; set; } = null!;
}
