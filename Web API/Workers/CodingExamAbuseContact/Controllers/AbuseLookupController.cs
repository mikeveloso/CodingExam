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

namespace CodingExamAbuseContact.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AbuseLookupController : ControllerBase
    {
        private readonly ILogger<AbuseLookupController> logger;
        private readonly ApiClient client;

        public AbuseLookupController(ILogger<AbuseLookupController> nlogger, ApiClient apiClient)
        {
            logger = nlogger;
            client = apiClient;
        }

        // GET api/abuselookup
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync(string host = "google.com")
        {
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(host);

                if (hostEntry != null)
                {
                    ApiResponse response = await client.GetAsync(hostEntry.HostName, ApiEndPointType.AbuseContactLookup);

                    if (response.IsSuccessStatusCode)
                    {
                        response.Result = JObject.Parse(response.Result.ToString().Replace("query", "AbuseLookup"));

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
            catch
            {
                return BadRequest(new { message = "Error parsing Domain or IP Address" });
            }
        }
    }
}
