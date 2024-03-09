using System.Net;

namespace IMGCloud.API.ViewModels
{
    public class BaseOption
    {
        public string Data { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
