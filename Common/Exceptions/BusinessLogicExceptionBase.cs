using Common.Enum;
using System.Net;

namespace Common.Exceptions
{
    public class BusinessLogicExceptionBase : Exception
    {
        public ErrorCodes ErrorCode { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public BusinessLogicExceptionBase(string message) : base(message)
        {

        }
    }
}
