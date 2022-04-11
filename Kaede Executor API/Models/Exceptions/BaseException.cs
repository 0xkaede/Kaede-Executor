using Newtonsoft.Json;
using System;

namespace Kaede_Executor_API.Models.Exceptions
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BaseException : Exception
    {
        [JsonProperty("errorType")]
        public string ErrorType => GetType().FullName.ToLower().Replace(".models", "");

        [JsonProperty("errorMessage")]
        public string ErrorMessage => base.Message;

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("errorVars")]
        public string[] ErrorVars { get; set; }

        public int StatusCode = 400;

        public BaseException(int code, string message, params string[] vars)
            : base(string.Format(message, vars))
        {
            ErrorCode = code;
            ErrorVars = vars;
        }
    }
}
