using Common.Enum;
using System.Net;

namespace Common.Exceptions
{
    public class ConferenceRoomNotFoundException : BusinessLogicExceptionBase
    {
        public ConferenceRoomNotFoundException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.ConferenceRoomNotFound;
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
