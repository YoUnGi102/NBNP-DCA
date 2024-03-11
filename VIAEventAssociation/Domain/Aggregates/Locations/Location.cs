using System;
using System.Collections.Generic;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace Domain.Aggregates.Locations;

public class Location
{
    public string name { get; init; }
    public int maxCapacity { get; init; }
    public List<DateTime> availability { get; init; }

    public Location(string name, int maxCapacity)
    {
        this.name = name;
        this.maxCapacity = maxCapacity;
        this.availability = new List<DateTime>();
    }
    
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
            return ResultFailure<Location>.CreateMessageResult(this, ["The name is too short!"]);
        }
        if (name.Length > 30)
        {
            return ResultFailure<Location>.CreateMessageResult(this, ["The name is too long!"]);
        }
        
        return ResultFailure<Location>.CreateSimpleResult(new Location(name, maxCapacity, availability));
    }

    public Result<Location> SetMaxCapacity(int maxCapacity)
    {
        if (name.Length <= 0)
        {
            return ResultFailure<Location>.CreateMessageResult(this, ["The capacity cannot be less or equal to zero!"]);
        }
        
        return ResultSuccess<Location>.CreateSimpleResult(new Location(name, maxCapacity, availability));
    }

    public Result<Location> SetAvailability(DateTime startDateTime, DateTime endDateTime)
    {
        if (startDateTime > endDateTime)
        {
            return ResultFailure<Location>.CreateMessageResult(this, ["The end date time cannot be before start date time"]);
        }
        return ResultSuccess<Location>.CreateSimpleResult(new Location(name, maxCapacity, availability));
    }

    public string GetName()
    {
        return name;
    }
    
    public int GetMaxCapacity()
    {
        return maxCapacity;
    }

    public List<DateTime> GetAvailability()
    {
        return availability;
    }
    
}