namespace Resume_Builder.Models
{
    public class Response
    {
        public bool HasError { get; set; } = false;

        public string Message { get; set; }

        public dynamic Data { get; set; }
    }
}
