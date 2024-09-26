using Common.Enum;
using System.Net;

namespace Common.Exceptions
{
    public class BookingNotFoundException : BusinessLogicExceptionBase
    {
        public BookingNotFoundException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.BookingNotFound;
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
