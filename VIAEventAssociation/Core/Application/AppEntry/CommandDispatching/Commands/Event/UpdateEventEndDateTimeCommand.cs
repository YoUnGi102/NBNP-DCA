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

        public static Result<UpdateEventEndDateTimeCommand> Create(Guid id, string endDateTime)
        {
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

            return ResultSuccess<UpdateEventEndDateTimeCommand>.CreateSimpleResult(new UpdateEventEndDateTimeCommand(id, parsedDate));
        }
    }
}