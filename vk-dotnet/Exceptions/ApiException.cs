using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace vk_dotnet
{
    /// <summary>
    /// Represents errors thrown by vk API;
    /// </summary>
    public class ApiException : Exception
    {

        /// <summary>
        /// Code of the error;
        /// For detailed info check https://vk.com/dev/errors
        /// </summary>


        /// <summary>
        /// Represents parameters that were send with request;
        /// </summary>




        public ApiException(int code, string message, List<RequestParam> requestedParams) : base(message)
        {
            Code = code;
            RequestedParams = requestedParams;
        }

        public int Code { get; private set; }
        public List<RequestParam> RequestedParams { get; private set; }
    }

    public class RequestParam
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class Error
    {
        /// <summary>
     /// Code of the error;
     /// For detailed info check https://vk.com/dev/errors
     /// </summary>
        public int error_code { get; set; }
        public string error_msg { get; set; }
        public List<RequestParam> request_params { get; set; }
    }
}
