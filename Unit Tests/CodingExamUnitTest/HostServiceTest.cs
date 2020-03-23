using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

using Moq;
using Moq.Protected;

using Xunit;

using CodingExam.HttpClients;
using CodingExam.Models;

namespace CodingExamUnitTest
{
    public class HostServiceTest
    {
        [Theory]
        [InlineData("google.com")]
        [InlineData("yahoo.com")]
        [InlineData("mjvonline.com")]
        public async Task ServiceClientTest(string host)
        {
            var mockLogger = new Mock<ILogger<ServiceClient>>();
            
            var client = new ServiceClient(mockLogger.Object);
            
            var response = await client.GetAsync(host);

            Assert.IsType<JObject>(response.Result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
