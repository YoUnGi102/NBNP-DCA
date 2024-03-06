namespace Domain.Aggregates.Locations;

public class Location
{
    public string name;
    public string maxCapacity;
    public List<DateTime> availability;
    
    public void UpdateName(int maxCapacity){}
    public void SetMaxCapacity(int maxCapacity){}
    
    public void SetAvailability(DateTime startDateTime, DateTime endDateTime){}
}