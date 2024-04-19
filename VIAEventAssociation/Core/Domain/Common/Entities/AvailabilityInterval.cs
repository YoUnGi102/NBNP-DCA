namespace Domain.Common.Entities;

public class AvailabilityInterval
{
    public int Id { get; private set; }
    public DateTime StartDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }

    public AvailabilityInterval(DateTime startDateTime, DateTime endDateTime)
    {
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
    }
    
    private AvailabilityInterval()
    {
        
    }
}