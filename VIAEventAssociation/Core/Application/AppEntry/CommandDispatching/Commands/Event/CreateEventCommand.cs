using Domain.Common.Enums;
using Domain.Common.Helpers;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event;

public class CreateEventCommand
{
    public string Title { get; }
    public string Description { get; }
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public int MaxGuests { get; }
    public EventVisibility Visibility { get; }
    public EventStatus Status { get; }
    public Guid LocationId { get; }

    public static Result<CreateEventCommand> Create(string title, string description, string startDateTime, string endDateTime, int maxGuests, string visibility, string status, Guid locationId)
    {
        DateTime? parsedEndDate, parsedStartDate;
        try
        {
            parsedStartDate = DateParser.ParseDate(startDateTime);
            parsedEndDate = DateParser.ParseDate(endDateTime);
        }
        catch (FormatException)
        {
            return ResultFailure<CreateEventCommand>.CreateMessageResult(null, ["The datetime format is incorrect"]);
        }
        
        if (string.IsNullOrWhiteSpace(title) || title.Length > 100 || title.Length < 3)
            return ResultFailure<CreateEventCommand>.CreateMessageResult(null, ["Title length is incorrect"]);

        if (description.Length < 11 || description.Length > 700)
            return ResultFailure<CreateEventCommand>.CreateMessageResult(null, ["Description length is incorrect"]);
        
        if (maxGuests <= 0)
            return ResultFailure<CreateEventCommand>.CreateMessageResult(null, ["Max guests must be greater than 0"]);

        EventVisibility parsedVisibility;
        bool parsed = Enum.TryParse(visibility, out parsedVisibility);
        if (!parsed)
        {
            return ResultFailure<CreateEventCommand>.CreateMessageResult(null, ["Visibility format is incorrect"]);
        }
        
        EventStatus parsedStatus;
        parsed = Enum.TryParse(status, out parsedStatus);

        if (!parsed)
        {
            return ResultFailure<CreateEventCommand>.CreateMessageResult(null, ["Status format is incorrect"]);
        }

        return ResultSuccess<CreateEventCommand>.CreateSimpleResult(new CreateEventCommand(title, description, (DateTime)parsedStartDate, (DateTime)parsedEndDate, maxGuests, parsedVisibility, parsedStatus, locationId));
    }

    private CreateEventCommand(string title, string description, DateTime startDateTime, DateTime endDateTime, int maxGuests, EventVisibility visibility, EventStatus status, Guid locationId)
        => (Title, Description, StartDateTime, EndDateTime, MaxGuests, Visibility, Status, LocationId) = (title, description, startDateTime, endDateTime, maxGuests, visibility, status, locationId);
}