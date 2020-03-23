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

namespace CodingReverseDNS.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReverseDnsController : ControllerBase
    {
        private readonly ILogger<ReverseDnsController> logger;
        private readonly ApiClient client;

        public ReverseDnsController(ILogger<ReverseDnsController> nlogger, ApiClient apiClient)
        {
            logger = nlogger;
            client = apiClient;
        }

        // GET api/reversedns
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync(string host = "google.com")
        {
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(host);

                if (hostEntry != null)
                {
                    string ipAddress = hostEntry.AddressList.FirstOrDefault().ToString();
                    
                    ApiResponse response = await client.GetAsync(ipAddress, ApiEndPointType.ReverseDnsLookup);

                    if (response.IsSuccessStatusCode)
                    {
                        response.Result = JObject.Parse(response.Result.ToString().Replace("query", "ReverseDNS"));

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
