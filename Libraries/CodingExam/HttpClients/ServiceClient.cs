using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

using CodingExam.Models;
using CodingExam.Common;
using System.Linq;
using CodingExam.Helpers;
using Newtonsoft.Json.Linq;
using System.Net;

namespace CodingExam.HttpClients
{
    public class ServiceClient : IServiceClient
    {
        private readonly ILogger logger;
        
        public ServiceClient(ILogger<ServiceClient> serviceLogger)
        {
            logger = serviceLogger;            
        }

        public async Task<ApiResponse> GetAsync(string host)
        {
            ApiResponse serviceResponse;

            JObject report = new JObject();

            try
            {

                foreach (Enum serviceType in Enum.GetValues(typeof(ServiceType)))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));

                   
                            string serviceEndpoint = ConfigurationHelper.GetConfiguration(serviceType.GetSectionKey());

                            client.BaseAddress = new Uri(serviceEndpoint);

                            HttpResponseMessage response = await client.GetAsync($"?host={host}");

                            if (response.IsSuccessStatusCode)
                            {
                                ApiResponse apiResponse = await ParseApiResponse(response);

                                JObject result = new JObject();
                                result.Add("statusCode", (int)response.StatusCode);
                                result.Add("results", apiResponse.Result);
                                report.Add(serviceType.ToString(), result);
                            }
                            else
                            {
                                JObject error = new JObject();
                                error.Add("statusCode", (int)response.StatusCode);
                                error.Add("errorMessage", response.ReasonPhrase);
                                report.Add(serviceType.ToString(), error);
                            }
                    
                    }
                }

                serviceResponse = new ApiResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Result = report
                };
            }
            catch (Exception ex)
            {
                serviceResponse = new ApiResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
            
            return serviceResponse;
        }

        private async Task<ApiResponse> ParseApiResponse(HttpResponseMessage response)
        {
            ApiResponse apiResponse = null;

            if (response.IsSuccessStatusCode)
            {
                string resultContent = await response.Content.ReadAsStringAsync();

                apiResponse = new ApiResponse()
                {
                    StatusCode = response.StatusCode,
                    Result = JObject.Parse(resultContent)
                };

                return apiResponse;
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.BadRequest)
                {
                    apiResponse = new ApiResponse()
                    {
                        StatusCode = response.StatusCode,
                        ErrorMessage = response.ReasonPhrase
                    };
                }
                else
                {
                    apiResponse = new ApiResponse()
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ErrorMessage = "Error service call"
                    };

                    logger.LogWarning("Failed service call for: {path}. StatusCode: {statusCode}; Reason: {reasonPhrase}", response.RequestMessage.RequestUri.PathAndQuery, response.StatusCode.ToString(), response.ReasonPhrase);
                }
            }

            return apiResponse;
        }
    }
}
