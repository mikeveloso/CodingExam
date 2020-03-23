using System;
using System.Collections.Generic;
using System.Text;

using CodingExam.Common;
using RestSharp;

namespace CodingExam.Helpers
{
    public class RapidApiSettingHelper
    {
        public static string RapidApiKey => ConfigurationHelper.GetConfiguration("RapidApiSettings", "ApiKey");

        public static string RapidApiResponseFormat => ConfigurationHelper.GetConfiguration("RapidApiSettings", "OutputType");

        public static string RapidHeaderHostParameter(ApiSettingsType setting) => ConfigurationHelper.GetConfiguration(string.Concat("RapidApiSettings:", setting.GetEnumValue().GetValue()), "HostName");

        public static string RapidApiBaseUri(ApiSettingsType setting, ApiEndPointType endpoint)
        {
            if (setting == ApiSettingsType.GeoLocationSettings)
            {
                return string.Concat("https://", RapidHeaderHostParameter(setting), "/");
            }
            else
            {
                return string.Concat("https://", RapidHeaderHostParameter(setting), "/", endpoint.GetEnumValue().GetValue().ToLowerInvariant(), "/");
            }
        }

        public static string RapidApiToken(ApiSettingsType setting) => ConfigurationHelper.GetConfiguration(string.Concat("RapidApiSettings:", setting.GetEnumValue().GetValue()), "ApiToken");

        public static string RapidApiRequestUri(ApiSettingsType setting, ApiEndPointType endpoint, string host)
        {
            string baseUri = RapidApiBaseUri(setting, endpoint);
            string requestUri = string.Empty;

            if (setting == ApiSettingsType.GeoLocationSettings)
            {
                
                requestUri = string.Concat(baseUri, host, endpoint.GetQueryStringParameters(host, RapidApiToken(setting), RapidApiResponseFormat));
            }
            else
            {
                requestUri = string.Concat(baseUri, endpoint.GetQueryStringParameters(host, RapidApiToken(setting), RapidApiResponseFormat));
            }
            
            return requestUri;
        }

        public static RestClient RapidApiHttpClient(ApiSettingsType setting, ApiEndPointType endpoint, string host)
        {
            return new RestClient(RapidApiRequestUri(setting, endpoint, host));
        }

        public static RestRequest RapidApiClientRequest(ApiSettingsType setting)
        {
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", RapidHeaderHostParameter(setting));
            request.AddHeader("x-rapidapi-key", RapidApiKey);

            return request;
        }

    }
}
