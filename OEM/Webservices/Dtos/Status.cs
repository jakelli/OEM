using System;
namespace OEM.Webservices.Dtos
{
    public class Status
    {
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public bool IsSuccess { get; set; }
    }
}
