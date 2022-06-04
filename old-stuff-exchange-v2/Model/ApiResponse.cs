namespace Old_stuff_exchange.Model
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Error { get; set; }
        public object Data { get; set; }
    }
}
