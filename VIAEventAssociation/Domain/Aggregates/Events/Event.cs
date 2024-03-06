﻿using System;
using System.Collections.Generic;
using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace Domain.Aggregates.Events;

public class Event
{
    private int id { get; init; }
    private string title { get; init; }
    private string description { get; init; }
    private DateTime start_date_time { get; init; }
    private DateTime end_date_time { get; init; }
    private int max_guests { get; init; }
    private EventVisibility visibility { get; set; }
    public EventStatus status { get; set; }
    private List<Guest> guests { get; init; }
    private Location location { get; init; }


    public Event(int id, string title, string description, DateTime start_date_time, DateTime end_date_time,
        int max_guests, EventVisibility visibility, EventStatus status, List<Guest> guests, Location location)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.start_date_time = start_date_time;
        this.end_date_time = end_date_time;
        this.max_guests = max_guests;
        this.visibility = visibility;
        this.status = status;
        this.guests = guests;
        this.location = location;
    }

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
        return ResultSuccess<Event>.CreateSimpleResult(new(id, title, description,
            start_date_time, end_date_time, max_guests, visibility, status, guests));
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
        return ResultSuccess<Event>.CreateSimpleResult(new(id, title, description,
            start_date_time, end_date_time, max_guests, visibility, status, guests, location));
    }

public Result<Event> SetVisibility(EventVisibility visibility)
    {
        return ResultSuccess<Event>.CreateSimpleResult(new(id, title, description,
            start_date_time, end_date_time, max_guests, visibility, status, guests, location));
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
        return ResultSuccess<Event>.CreateSimpleResult(new(id, title, description,
            startDateTime, end_date_time, max_guests, visibility, status, guests, location));
    }

    public Result<Event> UpdateEndDateTime(DateTime endDateTime)
    {
        if (endDateTime < DateTime.Now)
        {
            return ResultFailure<Event>.CreateMessageResult(this, new []{"The End time can't be in " +
                                                                         "the past!"});
        }

        if (endDateTime == start_date_time)
        {
            return ResultFailure<Event>.CreateMessageResult(this, new []{"The End time can't be the same" +
                                                                         " as the start time"});
        }
        return ResultSuccess<Event>.CreateSimpleResult(new(id, title, description,
            start_date_time, endDateTime, max_guests, visibility, status, guests));
    }
    
    public Result<Event> SetEventStatus(EventStatus status)
    {
        this.status = status;
        // string[] messages = new string[5];
        // if (status == this.status)
        //     return ResultSuccess<Event>.CreateMessageResult(this, new []{"Already in this status"});
        // if (1 < 2)
        //     messages.Append("1 is less than 2");
        // if (2 > 4)
        //     messages.Append("2 is smaller than 4");
        // if (messages.Length > 0)
        //     return ResultFailure<Event>.CreateMessageResult(this, messages);
        return ResultSuccess<Event>.CreateSimpleResult(new(id, title, description,
            start_date_time, end_date_time, max_guests, visibility, status, guests, location));
    }

    public Result<Event> SetMaxGuests(int maxGuests)
    {
        return ResultFailure<Event>.CreateEmptyResult();
    }

    public Result<Event> SetLocation(Location location)
    {
        return ResultFailure<Event>.CreateEmptyResult();
    }
    
    public int GetId()
    {
        return id;
    }
    
    public string GetTitle()
    {
        return title;
    }
    
    public string GetDescription()
    {
        return description;
    }
    
    public DateTime GetStartDateTime()
    {
        return start_date_time;
    }
    
    public DateTime GetEndDateTime()
    {
        return end_date_time;
    }
    
    public int GetMaxGuests()
    {
        return max_guests;
    }
    
    public EventVisibility GetVisibility()
    {
        return visibility;
    }
    
    public List<Guest> GetGuests()
    {
        return guests;
    }
    
    public Location GetLocation()
    {
        return location;
    }
}