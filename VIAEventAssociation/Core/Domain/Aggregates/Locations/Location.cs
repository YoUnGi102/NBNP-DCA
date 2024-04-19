using System;
using System.Collections.Generic;
using Domain.Common.Entities;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace Domain.Aggregates.Locations;

public class Location
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int MaxCapacity { get; init; }
    
    public List<AvailabilityInterval> Availability { get; init; }

    public Location(string name, int maxCapacity)
    {
        Name = name;
        MaxCapacity = maxCapacity;
        Availability = [];
    }
    
    // Needed for fakes
    public Location(int id, string name, int maxCapacity)
    {
        Id = id;
        Name = name;
        MaxCapacity = maxCapacity;
        Availability = [];
    }
    
    private Location(){}

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
        
        return ResultSuccess<Location>.CreateSimpleResult(new Location(name, MaxCapacity));
    }

    public Result<Location> SetMaxCapacity(int maxCapacity)
    {
        if (Name.Length <= 0)
        {
            return ResultFailure<Location>.CreateMessageResult(this, ["The capacity cannot be less or equal to zero!"]);
        }
        
        return ResultSuccess<Location>.CreateSimpleResult(new Location(Name, maxCapacity));
    }

    public Result<Location> SetAvailability(DateTime startDateTime, DateTime endDateTime)
    {
        
        if (startDateTime.CompareTo(endDateTime) > 0)
        {
            return ResultFailure<Location>.CreateMessageResult(this, ["The end date time cannot be before start date time"]);
        }
        
        AvailabilityInterval interval = new AvailabilityInterval(startDateTime, endDateTime);
        Availability.Add(interval);
        return ResultSuccess<Location>.CreateSimpleResult(new Location(Name, MaxCapacity));
    }
    
}