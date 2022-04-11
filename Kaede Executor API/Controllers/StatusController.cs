using Kaede_Executor_API.Models.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Kaede_Executor_API.Models.Status;

namespace Kaede_Executor_API.Controllers
{
    [ApiController]
    [Route("/Api")]
    public class StatusController : ControllerBase
    {
        readonly string ziplink = "https://cdn.discordapp.com/attachments/889423498945101837/963088090098958376/Debug.zip";

        [HttpGet]
        [Route("Status")]
        public async Task<ActionResult> GetReturn()
        {
            var JsonResponse = new EveryStatus
            {
                KEzip = ziplink,
                version = "1.0.0.0",
            };

            var result = JsonConvert.SerializeObject(JsonResponse);

            return Content(result, "application/json");
        }

        [HttpGet]
        [Route("Scripts")]
        public async Task<ActionResult> GetScriptsReturn()
        {
            var JsonResponse = new EveryStatus
            {
                Scripts = new List<Scripts>()
            };

            var result = JsonConvert.SerializeObject(JsonResponse);

            return Content(result, "application/json");
        }

        [HttpGet]
        [Route("All")]
        public async Task<ActionResult> GetAllReturn()
        {
            var JsonResponse = new EveryStatus
            {
                KEzip = ziplink,
                version = "1.0.0.0",
                Scripts = new List<Scripts>()
            };

            var result = JsonConvert.SerializeObject(JsonResponse);

            return Content(result, "application/json");
        }
    }
}
