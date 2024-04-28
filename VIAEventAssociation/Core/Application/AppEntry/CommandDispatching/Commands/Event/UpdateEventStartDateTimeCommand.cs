using Domain.Common.Helpers;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event
{
    public class UpdateEventStartDateTimeCommand
    {
        public Guid Id { get; }
        public DateTime StartDateTime { get; }

        private UpdateEventStartDateTimeCommand(Guid id, DateTime startDateTime)
        {
            Id = id;
            StartDateTime = startDateTime;
        }

        public static Result<UpdateEventStartDateTimeCommand> Create(string id, string startDateTime)
        {
            if (Guid.TryParse(id, out Guid eId) && eId != Guid.Empty)
                return ResultFailure<UpdateEventStartDateTimeCommand>.CreateMessageResult(null, ["EventId must be a valid Guid"]);

            DateTime parsedDate;
            try
            {
                parsedDate = DateParser.ParseDate(startDateTime);
            }
            catch (FormatException e)
            {
                return ResultFailure<UpdateEventStartDateTimeCommand>.CreateMessageResult(null, ["Incorrect Date Format."]);
            }
            
            if (parsedDate < DateTime.Now)
            {
                return ResultFailure<UpdateEventStartDateTimeCommand>.CreateMessageResult(null, ["Start Date Time cannot be in the past."]);
            }

            return ResultSuccess<UpdateEventStartDateTimeCommand>.CreateSimpleResult(new UpdateEventStartDateTimeCommand(eId, parsedDate));
        }
    }
}