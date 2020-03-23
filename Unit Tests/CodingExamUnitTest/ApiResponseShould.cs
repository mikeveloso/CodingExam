using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

using Xunit;

using CodingExam.Models;

namespace CodingExamUnitTest
{
    public class ApiResponseShould
    {
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.NoContent)]
        [InlineData(HttpStatusCode.Created)]
        public void ResponseSuccessWithSuccessStatusCodes(HttpStatusCode code)
        {
            ApiResponse response = new ApiResponse()
            {
                StatusCode = code
            };

            Assert.True(response.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.BadRequest)]
        public void ResponseFailedWithFailureStatusCodes(HttpStatusCode code)
        {
            ApiResponse response = new ApiResponse()
            {
                StatusCode = code
            };

            Assert.False(response.IsSuccessStatusCode);
        }
    }
}
