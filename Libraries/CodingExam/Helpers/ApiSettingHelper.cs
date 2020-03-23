using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using CodingExam.Common;

namespace CodingExam.Helpers
{
    public class ApiSettingHelper
    {
        public static string ApiKey => ConfigurationHelper.GetConfiguration("ApiSettings", "ApiKey");

        public static string OutputFormat => ConfigurationHelper.GetConfiguration("ApiSettings", "OutputFormat");

        public static string BaseUri => ConfigurationHelper.GetConfiguration("ApiSettings", "BaseUri");

        public static string RequestUri(ApiEndPointType endpoint, string host)
        {
            string queryString = endpoint.GetQueryStringParameters(host, ApiKey, OutputFormat);

            string requestUri = string.Empty;

            if (endpoint == ApiEndPointType.GeoIpLocation)
            {
                requestUri = $"{host}{queryString}";
            }
            else if (endpoint == ApiEndPointType.RdapLookup)
            {
                requestUri = $"{endpoint.GetEnumValue().GetValue()}{queryString}";
            }
            else if (endpoint == ApiEndPointType.DomainAvailability)
            {
                requestUri = $"{queryString}";
            }
            else 
            {
                requestUri = $"{endpoint.GetEnumValue().GetValue().ToLowerInvariant()}/{queryString}";
            }
            
            return requestUri;
        }        
    }
}
