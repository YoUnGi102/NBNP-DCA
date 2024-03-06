using Domain.Aggregates.Guests;
using Domain.Aggregates.Locations;
using Domain.Common.Enums;

namespace Domain.Aggregates.Events;

public class Event
{
    private int id;
    public string title;
    public string desctiption;
    public DateTime start_date_time;
    public DateTime end_date_time;
    private int max_guests;
    private EventVisibility visibility;
    public EventStatus Status;
    private List<Guest> guests;
    
    public void UpdateTitle(string title){}
    public void UpdateDescription(string description){}
    
    public void SetVisibility(EventVisibility visibility){}
    
    public void UpdateStartDateTime(DateTime startDateTime){}
    
    public void UpdateEndDateTime(DateTime endDateTime){}
    
    public void SetMaxGuests(int maxGuests){}
    
    public void SetLocation(Location location){}
}