using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using CodingExam.Helpers;
using CodingExam.Models;
using CodingExam.Common;

using Newtonsoft.Json.Linq;
using System.Net;

namespace CodingExam.HttpClients
{
    public class ApiClient : IApiClient
    {
        private readonly ILogger logger;
        private readonly HttpClient client;

        public ApiClient(ILogger<ApiClient> nlogger, HttpClient httpClient)
        {
            logger = nlogger;
            client = httpClient;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
        }

        public async Task<ApiResponse> GetAsync(string host, ApiEndPointType endpoint)
        {
            ApiResponse apiResponse;

            try
            {
                client.BaseAddress = new Uri(ApiSettingHelper.BaseUri);
                HttpResponseMessage response = await client.GetAsync(ApiSettingHelper.RequestUri(endpoint, host));
                
                if (response.IsSuccessStatusCode)
                {
                    string resultContent = await response.Content.ReadAsStringAsync();

                    apiResponse = new ApiResponse()
                    {
                        StatusCode = response.StatusCode,
                        Result = JObject.Parse(resultContent)
                    };
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        apiResponse = new ApiResponse()
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            ErrorMessage = response.ReasonPhrase
                        };
                    }
                    else
                    {
                        apiResponse = new ApiResponse()
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            ErrorMessage = "Error performing lookup"
                        };

                        logger.LogWarning("Failed lookup for: {lookup}. StatusCode: {statusCode}; Reason: {reasonPhrase}", host, response.StatusCode.ToString(), response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception occurred calling external API for '{lookup}'", host);

                apiResponse = new ApiResponse()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message,
                };
            }

            return apiResponse;
        }
    }
}
