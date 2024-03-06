using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace Domain.Aggregates.Events;

public class Event
{
    private int id { get; init; }
    private string title { get; init; }
    private string desctiption { get; init; }
    private DateTime start_date_time { get; init; }
    private DateTime end_date_time { get; init; }
    private int max_guests { get; init; }
    private EventVisibility visibility { get; set; }
    public EventStatus status { get; set; }
    private List<Guest> guests { get; init; }
    private Location location { get; init; }
    
    
    public Event(int id, string title, string desctiption, DateTime start_date_time, DateTime end_date_time, int max_guests, EventVisibility visibility, EventStatus status, List<Guest> guests)
    {
        this.id = id;
        this.title = title;
        this.desctiption = desctiption;
        this.start_date_time = start_date_time;
        this.end_date_time = end_date_time;
        this.max_guests = max_guests;
        this.visibility = visibility;
        this.status = status;
        this.guests = guests;
    }
    public void UpdateTitle(string title){}
    public void UpdateDescription(string description){}

    public void SetVisibility(EventVisibility visibility)
    {
        this.visibility = visibility;
    }
    
    public void UpdateStartDateTime(DateTime startDateTime){}
    
    public void UpdateEndDateTime(DateTime endDateTime){}
    
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
        return ResultSuccess<Event>.CreateSimpleResult(new(id, title, desctiption,
            start_date_time, end_date_time, max_guests, visibility, status, guests));
    }
    
    public void SetMaxGuests(int maxGuests){}
    
    public void SetLocation(Location location){}
    
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
        return desctiption;
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