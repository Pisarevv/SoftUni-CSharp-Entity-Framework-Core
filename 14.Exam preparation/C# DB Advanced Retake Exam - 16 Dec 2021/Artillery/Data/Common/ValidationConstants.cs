namespace Artillery.Data.Common;
public class ValidationConstants
{
    //Country
    public const int MaxCountryNameLength = 60;
    public const int MinCountryNameLength = 4;

    public const int MaxArmySize = 10000000;
    public const int MinArmySize = 50000;

    //Manufacturer
    public const int MaxManufacturerNameLength = 40;
    public const int MinManufacturerNameLength = 4;

    public const int MaxFoundedTextLength = 100;
    public const int MinFoundedTextLength = 10;

    //Shell
    public const double MaxShellWeight = 1680.00;
    public const double MinShellWeight = 2.00;

    public const int MaxCaliberLength = 30;
    public const int MinCaliberLength = 4;

    //Gun
    public const int MaxGunWeight = 1350000;
    public const int MinGunWeight = 100;

    public const double MaxBarrelLength = 35.00;
    public const double MinBarrelLength = 2.00;

    public const int MaxGunRange = 100000;
    public const int MinGunRange = 1;

 
}
