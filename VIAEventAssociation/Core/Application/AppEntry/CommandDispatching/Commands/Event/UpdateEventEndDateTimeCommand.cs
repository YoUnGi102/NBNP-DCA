using Domain.Common.Helpers;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event
{
    public class UpdateEventEndDateTimeCommand
    {
        public Guid Id { get; }
        public DateTime EndDateTime { get; }

        private UpdateEventEndDateTimeCommand(Guid id, DateTime endDateTime)
        {
            Id = id;
            EndDateTime = endDateTime;
        }

        public static Result<UpdateEventEndDateTimeCommand> Create(string id, string endDateTime)
        {
            if (Guid.TryParse(id, out Guid eId) && eId != Guid.Empty)
                return ResultFailure<UpdateEventEndDateTimeCommand>.CreateMessageResult(null, ["EventId must be a valid Guid"]);

            DateTime parsedDate;
            try
            {
                parsedDate = DateParser.ParseDate(endDateTime);
            }
            catch (FormatException e)
            {
                return ResultFailure<UpdateEventEndDateTimeCommand>.CreateMessageResult(null, ["Incorrect Date Format."]);
            }
            
            if (parsedDate < DateTime.Now)
            {
                return ResultFailure<UpdateEventEndDateTimeCommand>.CreateMessageResult(null, ["End Date Time cannot be in the past."]);
            }

            return ResultSuccess<UpdateEventEndDateTimeCommand>.CreateSimpleResult(new UpdateEventEndDateTimeCommand(eId, parsedDate));
        }
    }
}