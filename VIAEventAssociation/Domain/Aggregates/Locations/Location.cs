using System;
using System.Collections.Generic;
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

    public Result<Location> UpdateName(string name)
    {
        if (name.Length < 3)
        {
            return ResultFailure<Location>.CreateMessageResult(this, new[]{"The name is too short!"});
        }
        if (name.Length > 30)
        {
            return ResultFailure<Location>.CreateMessageResult(this, new[]{"The name is too long!"});
        }
        
        return ResultFailure<Location>.CreateSimpleResult(new Location(name, maxCapacity, availability));
    }

    public Result<Location> SetMaxCapacity(int maxCapacity)
    {
        return ResultFailure<Location>.CreateEmptyResult();
    }

    public Result<Location> SetAvailability(DateTime startDateTime, DateTime endDateTime)
    {
        return ResultFailure<Location>.CreateEmptyResult();
    }

    public string GetName()
    {
        return name;
    }
    
}