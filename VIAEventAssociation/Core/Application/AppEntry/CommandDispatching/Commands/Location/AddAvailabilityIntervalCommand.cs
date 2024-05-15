using Domain.Common.Helpers;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class AddAvailabilityIntervalCommand
{
    public Guid LocationId { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public static Result<AddAvailabilityIntervalCommand> Create(Guid locationId, string startDateString, string endDateString)
    {
        DateTime startDate;
        DateTime endDate;
        try
        {
            startDate = DateParser.ParseDate(startDateString);
            endDate = DateParser.ParseDate(endDateString);
        }
        catch (FormatException)
        {
            return ResultFailure<AddAvailabilityIntervalCommand>.CreateMessageResult(null, ["Invalid date format"]);
        }

        if (startDate >= endDate)
            return ResultFailure<AddAvailabilityIntervalCommand>.CreateMessageResult(null, ["StartDate must be less than EndDate"]);

        return ResultSuccess<AddAvailabilityIntervalCommand>.CreateSimpleResult(new AddAvailabilityIntervalCommand(locationId, startDate, endDate));
    }

    private AddAvailabilityIntervalCommand(Guid locationId, DateTime startDate, DateTime endDate)
        => (LocationId, StartDate, EndDate) = (locationId, startDate, endDate);
}