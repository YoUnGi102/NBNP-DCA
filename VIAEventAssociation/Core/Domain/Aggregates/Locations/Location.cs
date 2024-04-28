using System;
using Domain.Common.Entities;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace Domain.Aggregates.Locations;

public class Location
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public int MaxCapacity { get; init; }
    public DateOnly AvailabilityStart { get; private set; }
    public DateOnly? AvailabilityEnd { get; private set; }
    
    public Location(string id, string name, int maxCapacity)
    {
        Id = Guid.TryParse(id, out var guid) ? guid : Guid.NewGuid();
        Name = name;
        MaxCapacity = maxCapacity;
        AvailabilityStart = DateOnly.FromDateTime(DateTime.Now);
        AvailabilityEnd = null;
    }
    
    public Location(string name, int maxCapacity)
    {
        Id = Guid.NewGuid();
        Name = name;
        MaxCapacity = maxCapacity;
        AvailabilityStart = DateOnly.FromDateTime(DateTime.Now);
        AvailabilityEnd = null;
    }
    
    public Location(string name, int maxCapacity, DateOnly availabilityStart, DateOnly availabilityEnd)
    {
        Id = Guid.NewGuid();
        Name = name;
        MaxCapacity = maxCapacity;
        AvailabilityStart = availabilityStart;
        AvailabilityEnd = availabilityEnd;
    }
    
    // Needed for fakes
    public Location(Guid id, string name, int maxCapacity, DateOnly availabilityStart, DateOnly availabilityEnd)
    {
        Id = id;
        Name = name;
        MaxCapacity = maxCapacity;
        AvailabilityStart = availabilityStart;
        AvailabilityEnd = availabilityEnd;
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
        
        AvailabilityStart = DateOnly.FromDateTime(startDateTime);
        AvailabilityEnd = DateOnly.FromDateTime(endDateTime);
        return ResultSuccess<Location>.CreateSimpleResult(new Location(Name, MaxCapacity));
    }
    
}