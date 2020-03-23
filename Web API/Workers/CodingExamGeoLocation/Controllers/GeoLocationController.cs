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

namespace CodingExamGeoLocation.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GeoLocationController : ControllerBase
    {
        private readonly ILogger<GeoLocationController> logger;
        private readonly ApiClient client;

        public GeoLocationController(ILogger<GeoLocationController> nlogger, ApiClient apiClient)
        {
            logger = nlogger;
            client = apiClient;
        }

        // GET api/geolocation
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync(string host = "google.com")
        {
            if (Uri.CheckHostName(host) == UriHostNameType.Dns || Uri.CheckHostName(host) == UriHostNameType.IPv4)
            {
                ApiResponse response = await client.GetAsync(host, ApiEndPointType.GeoIpLocation);

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
