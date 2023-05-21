using System.Net;

namespace Domain.Response
{
    public class ResponseData<T>
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;

        public ResponseData(T data)
        {
            Success = true;
            StatusCode = HttpStatusCode.OK;
            Data = data;
            Message = "Operación realizada correctamente";
        }

        public ResponseData(HttpStatusCode httpStatusCode, string message)
        {
            Success = false;
            StatusCode = httpStatusCode;
            Message = message;
        }
    }
}
