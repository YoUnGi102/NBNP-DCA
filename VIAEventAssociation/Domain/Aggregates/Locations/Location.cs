using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace Domain.Aggregates.Locations;

public class Location
{
    public string name { get; init; }
    public int maxCapacity { get; init; }
    public List<DateTime> availability { get; init; }

    public Location(string name, int maxCapacity, List<DateTime> availability)
    {
        this.name = name;
        this.maxCapacity = maxCapacity;
        this.availability = availability;
    }

    public Result<Location> UpdateName(int maxCapacity)
    {
        return ResultFailure<Location>.CreateEmptyResult();
    }

    public Result<Location> SetMaxCapacity(int maxCapacity)
    {
        return ResultFailure<Location>.CreateEmptyResult();
    }

    public Result<Location> SetAvailability(DateTime startDateTime, DateTime endDateTime)
    {
        return ResultFailure<Location>.CreateEmptyResult();
    }
}