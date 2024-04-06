using System;
using Domain.Common.Helpers;
using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace ViaEventAssociation.Core.Application.Features
{
    public class UpdateEventEndDateTimeCommand
    {
        public int Id { get; }
        public DateTime EndDateTime { get; }

        private UpdateEventEndDateTimeCommand(int id, DateTime endDateTime)
        {
            Id = id;
            EndDateTime = endDateTime;
        }

        public static Result<UpdateEventEndDateTimeCommand> Create(int id, string endDateTime)
        {
            if (id <= 0)
            {
                return ResultFailure<UpdateEventEndDateTimeCommand>.CreateMessageResult(null, ["Id must be greater than 0."]);
            }

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