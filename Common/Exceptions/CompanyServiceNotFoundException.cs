using Common.Enum;
using System.Net;

namespace Common.Exceptions
{
    public class CompanyServiceNotFoundException : BusinessLogicExceptionBase
    {
        public CompanyServiceNotFoundException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.CompanyServiceNotFound;
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
