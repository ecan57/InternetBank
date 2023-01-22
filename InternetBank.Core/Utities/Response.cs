using System;

namespace InternetBank.Core.Utities
{
    public class Response
    {
        public string ResponseCode { get; set; }
        public string RequestId => $"{Guid.NewGuid()}";
        public string ResponseMessage { get; set; }
    }
}
