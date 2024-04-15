using System.Net;

namespace IntegrationServer.Api
{
    public class Http_ClientResponse
    {
        public object responseBody { get; set; }
        public HttpStatusCode statusCode { get; set; }
    }
}
