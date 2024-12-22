using System.Net;

namespace MinimalApi_Sample
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessage = new List<string>();
        }
        public bool IsSuccess { get; set; }
        public object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
}
