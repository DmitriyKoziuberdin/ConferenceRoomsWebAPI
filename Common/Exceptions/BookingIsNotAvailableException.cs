using Common.Enum;
using System.Net;

namespace Common.Exceptions
{
    public class BookingIsNotAvailableException :BusinessLogicExceptionBase
    {
        public BookingIsNotAvailableException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.BookingIsNotAvailable;
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
