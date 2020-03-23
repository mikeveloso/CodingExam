using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json.Linq;

using CodingExam.Common;
using CodingExam.HttpClients;
using CodingExam.Models;

namespace CodingExamReverseIp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReverseIpController : ControllerBase
    {
        private readonly ILogger<ReverseIpController> logger;
        private readonly ApiClient client;

        public ReverseIpController(ILogger<ReverseIpController> nlogger, ApiClient apiClient)
        {
            logger = nlogger;
            client = apiClient;
        }

        // GET api/reverseip
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync(string host = "google.com")
        {
            try
            {
                ApiResponse response = await client.GetAsync(host, ApiEndPointType.ReverseIp);

                if (response.IsSuccessStatusCode)
                {
                    response.Result = JObject.Parse(response.Result.ToString().Replace("query", "ReverseIp"));

                    return Ok(response.Result);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(new { message = response.ErrorMessage });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { message = response.ErrorMessage });
                }                
            }
            catch
            {
                return BadRequest(new { message = "Error parsing Domain or IP Address" });
            }
        }
    }
}
