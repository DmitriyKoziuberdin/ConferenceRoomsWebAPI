using Common.Enum;
using System.Net;

namespace Common.Exceptions
{
    public class ConferenceRoomDuplicateNameException :BusinessLogicExceptionBase
    {
        public ConferenceRoomDuplicateNameException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.ConferenceRoomDuplicateName;
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}
