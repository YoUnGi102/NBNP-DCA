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
        int max_guests, EventVisibility visibility, EventStatus status, List<Guest> guests)
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
    }

    public Result<Event> UpdateTitle(string title)
    {
        return ResultFailure<Event>.CreateEmptyResult();
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
            start_date_time, end_date_time, max_guests, visibility, status, guests));
    }

public Result<Event> SetVisibility(EventVisibility visibility)
    {
        return ResultFailure<Event>.CreateEmptyResult();
    }

    public Result<Event> UpdateStartDateTime(DateTime startDateTime)
    {
        return ResultFailure<Event>.CreateEmptyResult();
    }

    public Result<Event> UpdateEndDateTime(DateTime endDateTime)
    {
        return ResultFailure<Event>.CreateEmptyResult();
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
            start_date_time, end_date_time, max_guests, visibility, status, guests));
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