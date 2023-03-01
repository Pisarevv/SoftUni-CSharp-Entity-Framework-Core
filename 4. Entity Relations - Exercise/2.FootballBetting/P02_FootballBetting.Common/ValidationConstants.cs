namespace P02_FootballBetting.Common;

public class ValidationConstants
{
    //Team
    public const int MaxTeamNameLength = 100;
    public const int MaxLogoUrlLength = 2056;
    public const int MaxInitialsLength = 5;

    //Color
    public const int MaxColorNameLength = 10;

    //Town
    public const int MaxTownNameLength = 10;

    //Country
    public const int MaxCountryNameLength = 10;

    //Player
    public const int MaxPlayerNameLength = 100;
    public const int MaxSquadNameLength = 100;

    //Position
    public const int MaxPositionNameLength = 20;

    //Game
    public const int MaxGameResultLength = 5;

    //Bet
    public const int MaxPredictionLength = 5;

    //User
    public const int MaxUsernameLength = 36;
    public const int MaxPasswordLength = 32;
    public const int MaxEmailLength = 64;
}
