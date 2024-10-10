using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.API.Responses
{
    public class BaseResponse
    {
        public bool Success {get; set;}
        public string Message { get; set; }
        public List<string>? ValidationErrors { get; set; }

        public BaseResponse()
        {
            Success = true;
        }

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

    }
}