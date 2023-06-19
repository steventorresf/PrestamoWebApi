using System.Net;

namespace Domain.Response
{
    public class ResponseData<T>
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;

        public ResponseData(T data, string message)
        {
            Success = true;
            Data = data;
            Message = message;
        }

        public ResponseData(HttpStatusCode httpStatusCode, string message)
        {
            Success = false;
            StatusCode = httpStatusCode;
            Message = message;
        }
    }
}
