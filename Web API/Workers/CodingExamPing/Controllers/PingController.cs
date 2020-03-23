using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using CodingExam.Common;
using CodingExam.HttpClients;
using CodingExam.Models;

namespace CodingExamPing.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly ILogger<PingController> logger;
        private readonly ApiClient client;

        public PingController(ILogger<PingController> nlogger, ApiClient apiClient)
        {
            logger = nlogger;
            client = apiClient;
        }

        // GET api/ping
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync(string host = "google.com")
        {
            if (Uri.CheckHostName(host) == UriHostNameType.Dns || Uri.CheckHostName(host) == UriHostNameType.IPv4)
            {
                ApiResponse response = await client.GetAsync(host, ApiEndPointType.Ping);

                if (response.IsSuccessStatusCode)
                {
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
            else
            {
                return BadRequest(new { message = "Error parsing Domain or IP Address" });
            }
        }
    }
}
