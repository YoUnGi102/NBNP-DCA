using System;
using System.Collections.Generic;
using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Entities;
using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace Domain.Aggregates.Events;

public class Event
{
    public Guid Id { get; private set;}
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime StartDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }
    public int MaxGuests { get; private set; }
    public EventVisibility Visibility { get; private set; }
    public EventStatus Status { get; set; }
    public List<Guest> Guests { get; private set; } = [];
    public Location Location { get; private set; }
    public List<Invitation> Invitations { get; private set; }
    public List<Request> Requests { get; private set; }

    
    public Event(string title, string description, DateTime startDateTime, DateTime endDateTime,
        int maxGuests, EventVisibility visibility, EventStatus status, List<Guest> guests, Location location)
    {
        Title = title;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        MaxGuests = maxGuests;
        Visibility = visibility;
        Status = status;
        Guests = guests;
        Location = location;
    }
    
    // Needed for Fake Repositories in Unit Tests
    public Event(Guid id, string title, string description, DateTime startDateTime, DateTime endDateTime,
        int maxGuests, EventVisibility visibility, EventStatus status, List<Guest> guests, Location location)
    {
        Id = id;
        Title = title;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        MaxGuests = maxGuests;
        Visibility = visibility;
        Status = status;
        Guests = guests;
        Location = location;
    }
    
    // Needed for EFC
    private Event(){}

    public Result<Event> UpdateTitle(string title)
    {
        if (title.Length < 3)
        {
            return ResultFailure<Event>.CreateMessageResult(this, new []{"The title can't be smaller than" +
                                                                         " 5 characters!"});
        }

        if (title.Length > 100)
        {
            return ResultFailure<Event>.CreateMessageResult(this, new []{"The title can't be larger than" +
                                                                         " 100 characters!"});   
        }
        return ResultSuccess<Event>.CreateSimpleResult(new(Id, title, Description,
            StartDateTime, EndDateTime, MaxGuests, Visibility, Status, Guests, Location));
    }

    public Result<Event> UpdateDescription(string description)
    {
        if (description.Length < 11)
        {
            return ResultFailure<Event>.CreateMessageResult(this, new[]{"The length of the description is" +
                                                                        " too small!"});
        }

        if (description.Length > 700)
        {
            return ResultFailure<Event>.CreateMessageResult(this, new []{"The length of the description is" +
                                                                         " too big!"});
        }
        return ResultSuccess<Event>.CreateSimpleResult(new(Id, Title, description,
            StartDateTime, EndDateTime, MaxGuests, Visibility, Status, Guests, Location));
    }

    public Result<None> AddGuest(Guest guest)
    {
        if (Status != EventStatus.Active)
        {
            return ResultFailure<None>.CreateMessageResult(new None(), ["The event is not active!"]);
        }
        
        if (Guests.Contains(guest))
        {
            return ResultFailure<None>.CreateMessageResult(new None(), new []{"The guest is already in the list"});
        }
        
        if (MaxGuests <= Guests.Count)
        {
            return ResultFailure<None>.CreateMessageResult(new None(), new []{"The event is full"});
        }
        
        Guests.Add(guest);
        return ResultSuccess<None>.CreateSimpleResult(new None());
    }

    public Result<None> RemoveGuest(Guest guest)
    {
        if(!Guests.Contains(guest))
        {
            return ResultFailure<None>.CreateMessageResult(new None(), new []{"The guest is not in the list"});
        }

        Guests.Remove(guest);
        return ResultSuccess<None>.CreateSimpleResult(new None());
    }

    public Result<Event> SetVisibility(EventVisibility visibility)
    {
        return ResultSuccess<Event>.CreateSimpleResult(new(Id, Title, Description,
            StartDateTime, EndDateTime, MaxGuests, visibility, Status, Guests, Location));
    }

    public Result<Event> UpdateStartDateTime(DateTime startDateTime)
    {
        if (startDateTime < DateTime.Now)
        {
            return ResultFailure<Event>.CreateMessageResult(this, new []{"Cannot assign a date in the past" +
                                                                         " as a start date time"});
        }

        if (startDateTime > DateTime.Now.AddYears(35))
        {
            return ResultFailure<Event>.CreateMessageResult(this, new []{"Cannot assign a date too far" +
                                                                         " into the future as a start date time"});
        }
        return ResultSuccess<Event>.CreateSimpleResult(new(Id, Title, Description,
            startDateTime, EndDateTime, MaxGuests, Visibility, Status, Guests, Location));
    }

    public Result<Event> UpdateEndDateTime(DateTime endDateTime)
    {
        if (endDateTime < DateTime.Now)
        {
            return ResultFailure<Event>.CreateMessageResult(this, new []{"The End time can't be in " +
                                                                         "the past!"});
        }

        if (endDateTime == StartDateTime)
        {
            return ResultFailure<Event>.CreateMessageResult(this, new []{"The End time can't be the same" +
                                                                         " as the start time"});
        }
        return ResultSuccess<Event>.CreateSimpleResult(new(Id, Title, Description,
            StartDateTime, endDateTime, MaxGuests, Visibility, Status, Guests, Location));
    }
    
    public Result<Event> SetEventStatus(EventStatus status)
    {
        this.Status = status;
        // string[] messages = new string[5];
        // if (status == this.status)
        //     return ResultSuccess<Event>.CreateMessageResult(this, new []{"Already in this status"});
        // if (1 < 2)
        //     messages.Append("1 is less than 2");
        // if (2 > 4)
        //     messages.Append("2 is smaller than 4");
        // if (messages.Length > 0)
        //     return ResultFailure<Event>.CreateMessageResult(this, messages);
        return ResultSuccess<Event>.CreateSimpleResult(new(Id, Title, Description,
            StartDateTime, EndDateTime, MaxGuests, Visibility, status, Guests, Location));
    }

    public Result<Event> SetMaxGuests(int maxGuests)
    {
        if (maxGuests < 1)
        {
            return ResultFailure<Event>.CreateMessageResult(this, new []{"The maximum number of guests" +
                                                                         " should be a positive number hher than 0"});
        }

        if (maxGuests > Location.MaxCapacity + 1)
        {
            return ResultFailure<Event>.CreateMessageResult(this, new []{"The amount of users can't be" +
                                                                         " higher than the number of places at the" +
                                                                         " chosen location. The number of places" +
                                                                         " is:" + Location.MaxCapacity});
        }
        return ResultSuccess<Event>.CreateSimpleResult(new(Id, Title, Description,
            StartDateTime, EndDateTime, maxGuests, Visibility, Status, Guests, Location));
    }

    public Result<Event> SetLocation(Location location)
    {

        if (location.MaxCapacity < MaxGuests)
        {
            return ResultFailure<Event>.CreateMessageResult(this, ["The event guest capacity cannot exceed location capacity"]);
        }
        
        return ResultSuccess<Event>.CreateSimpleResult(new Event(Id, Title, Description,
            StartDateTime, EndDateTime, MaxGuests, Visibility, Status, Guests, location));
    }

    // public override bool Equals(Object other)
    // {
    //     Event b = (Event)other;
    //
    //     return Id == b.Id &&
    //            Title == b.Title &&
    //            Description == b.Description &&
    //            StartDateTime == b.StartDateTime &&
    //            EndDateTime == b.EndDateTime &&
    //            MaxGuests == b.MaxGuests &&
    //            Visibility == b.Visibility &&
    //            Status == b.Status;
    // }
}