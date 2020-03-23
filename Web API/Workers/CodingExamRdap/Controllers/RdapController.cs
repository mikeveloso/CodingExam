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
using Newtonsoft.Json.Linq;

namespace CodingExamRdap.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RdapController : ControllerBase
    {
        private readonly ILogger<RdapController> _logger;
        private readonly ApiClient client;

        public RdapController(ILogger<RdapController> logger, ApiClient apiClient)
        {
            _logger = logger;
            client = apiClient;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync(string host = "google.com")
        {
            if (Uri.CheckHostName(host) == UriHostNameType.Dns || Uri.CheckHostName(host) == UriHostNameType.IPv4)
            {
                ApiResponse response = await client.GetAsync(host, ApiEndPointType.RdapLookup);

                if (response.IsSuccessStatusCode)
                {
                    string jObjectString = response.Result.ToString(Newtonsoft.Json.Formatting.None).Replace("WhoisRecord", "RDAPRecord");

                    response.Result = JObject.Parse(jObjectString);

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
