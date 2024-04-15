using IntegrationServer.Api.Enums;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationServer.Api.Services
{
    public interface IIntegrationClientFactory
    {
        Task<Http_ClientResponse> RequestIntegrationApiAsync(string url, Type className, object requestBody, IntegrationMethodsEnums integrationMethodsEnumsstring, string headerToken);
    }
}
