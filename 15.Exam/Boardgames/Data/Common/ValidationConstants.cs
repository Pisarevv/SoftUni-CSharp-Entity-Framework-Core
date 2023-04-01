namespace Boardgames.Data.Common;

public class ValidationConstants
{
    //Boardgame
    public const int MaxBoardgameNameLength = 20;
    public const int MinBoardgameNameLength = 10;

    public const double MaxBoardgameRating = 10.00;
    public const double MinBoardgameRating = 1.00;

    public const int MaxYearPublishedBoardgame = 2023;
    public const int MinYearPublishedBoardgame = 2018;

    //Seller
    public const int MaxSellerNameLength = 20;
    public const int MinSellerNameLength = 5;

    public const int MaxSellerAddressLength = 30;
    public const int MinSellerAddressLength = 2;


    //Creator
    public const int MaxCreatorFirstNameLength = 7;
    public const int MinCreatorFirstNameLength = 2;

    public const int MaxCreatorLastNameLength = 7;
    public const int MinCreatorLastNameLength = 2;

}
