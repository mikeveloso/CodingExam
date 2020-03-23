using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using CodingExam.HttpClients;
using CodingExam.Helpers;
using CodingExam.Models;
using Newtonsoft.Json.Linq;
using CodingExam.Common;

namespace CodingExamApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class HostServiceController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IServiceClient client;
        

        public HostServiceController(ILogger<HostServiceController> logger, IServiceClient serviceClient)
        {
            _logger = logger;
            client = serviceClient;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync(string host = "google.com")
        {
            ApiResponse response;

            try
            {
                response = await client.GetAsync(host);

                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
