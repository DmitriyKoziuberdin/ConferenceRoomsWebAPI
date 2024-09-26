using Common.Enum;
using System.Net;

namespace Common.Exceptions
{
    public class CompanyServiceDuplicateNameException : BusinessLogicExceptionBase
    {
        public CompanyServiceDuplicateNameException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.CompanyServiceDuplicateName;
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}
