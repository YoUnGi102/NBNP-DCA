using Domain.Common.Helpers;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features;

public class AddAvailabilityIntervalCommand
{
    public int LocationId { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public static Result<AddAvailabilityIntervalCommand> Create(int locationId, string startDateString, string endDateString)
    {
        if (locationId <= 0)
            return ResultFailure<AddAvailabilityIntervalCommand>.CreateMessageResult(null, ["LocationId must be greater than 0"]);

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

    private AddAvailabilityIntervalCommand(int locationId, DateTime startDate, DateTime endDate)
        => (LocationId, StartDate, EndDate) = (locationId, startDate, endDate);
}