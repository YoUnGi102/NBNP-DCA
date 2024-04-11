using Domain.Common.Helpers;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.AppEntry.CommandDispatching.Commands.Event
{
    public class UpdateEventStartDateTimeCommand
    {
        public int Id { get; }
        public DateTime StartDateTime { get; }

        private UpdateEventStartDateTimeCommand(int id, DateTime startDateTime)
        {
            Id = id;
            StartDateTime = startDateTime;
        }

        public static Result<UpdateEventStartDateTimeCommand> Create(int id, string startDateTime)
        {
            if (id <= 0)
            {
                return ResultFailure<UpdateEventStartDateTimeCommand>.CreateMessageResult(null, ["Id must be greater than 0."]);
            }

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

            return ResultSuccess<UpdateEventStartDateTimeCommand>.CreateSimpleResult(new UpdateEventStartDateTimeCommand(id, parsedDate));
        }
    }
}