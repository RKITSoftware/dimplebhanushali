using System.Data;

namespace Historical_Events.Models
{
    public class Response
    {
        public bool isError { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}