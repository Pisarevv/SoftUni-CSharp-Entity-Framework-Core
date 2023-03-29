namespace Trucks.Data.Common;
public class ValidationConstants
{
    //Truck
    public const int MaxRegistrationNumberLength = 8;
    public const int MinRegistrationNumberLength = 8;

    public const int MaxVinNumberLength = 17;
    public const int MinVinNumberLength = 17;

    public const int MaxTankCapacity = 1420;
    public const int MinTankCapacity = 950;

    public const int MaxCargoCapacity = 29000;
    public const int MinMaxCargoCapacity = 5000;

    //Client
    public const int MaxClientNameLength = 40;
    public const int MinClientNameLength = 3;

    public const int MaxClientNationalityLength = 40;
    public const int MinClientNationalityLength = 2;

    //Dispatcher
    public const int MaxDispatcherNameLength = 40;
    public const int MinDispacherNameLength = 2;
}
