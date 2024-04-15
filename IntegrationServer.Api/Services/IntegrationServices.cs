using IntegrationServer.Api.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace IntegrationServer.Api.Services
{
    public class IntegrationServices : IIntegrationClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IntegrationServices> _logger;


        public IntegrationServices(IHttpClientFactory httpClientFactory, ILogger<IntegrationServices> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<Http_ClientResponse> RequestIntegrationApiAsync(string url, Type className, object requestBody, IntegrationMethodsEnums integrationMethodsEnums, string headerToken)
        {
            try
            {
                HttpClient _httpClient = _httpClientFactory.CreateClient();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (headerToken != null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", headerToken);
                }

                var jsonContent = JsonSerializer.Serialize(requestBody);
                HttpResponseMessage response;

                switch (integrationMethodsEnums)
                {
                    case IntegrationMethodsEnums.PostMethod:
                        response = await _httpClient.PostAsync(url, new StringContent(jsonContent, Encoding.UTF8, "application/json"));
                        break;
                    case IntegrationMethodsEnums.PutMethod:
                        response = await _httpClient.PutAsync(url, new StringContent(jsonContent, Encoding.UTF8, "application/json"));
                        break;
                    case IntegrationMethodsEnums.DeleteMethod:
                        response = await _httpClient.DeleteAsync(url); //Cancel token islenebilir...
                        break;
                    default:
                        response = await _httpClient.GetAsync(url);
                        break;
                }


                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize(responseBody, className);
                    return new Http_ClientResponse() { responseBody = data, statusCode = response.StatusCode };
                }
                else
                {
                    return new Http_ClientResponse() { statusCode = response.StatusCode };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Concat("Istek islenirken hata olustu:", ex.Message));
                return new Http_ClientResponse() { statusCode = System.Net.HttpStatusCode.NoContent };
            }
        }
    }


}
