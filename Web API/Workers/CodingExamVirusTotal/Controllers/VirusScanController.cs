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

namespace CodingExamVirusTotal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class VirusScanController : ControllerBase
    {
        private readonly ILogger<VirusScanController> logger;
        private readonly IVirusTotalClient client;

        public VirusScanController(ILogger<VirusScanController> nlogger, IVirusTotalClient virusTotalClient)
        {
            logger = nlogger;
            client = virusTotalClient;
        }

        // GET api/virustotal
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync(string host = "google.com")
        {
            try
            {
                ApiResponse response = await client.GetAsync(host);

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
            catch
            {
                return BadRequest(new { message = "Error parsing Domain or IP Address" });
            }
        }
    }
}
