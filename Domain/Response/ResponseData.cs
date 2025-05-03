namespace Domain.Response
{
    public class ResponseData<T>
    {
        public bool Success { get; set; } = true;
        
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;

        public ResponseData() { }
        public ResponseData(T data, string message)
        {
            Data = data;
            Message = message;
        }
    }
}
