using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Tunein.API.Fake.Entities;

namespace Tunein.API.Fake.Controllers
{
    [ApiController]
    [Route("")]
    public class TuneinFakeController : ControllerBase
    {
        private readonly ILogger<TuneinFakeController> _logger;

        public TuneinFakeController(ILogger<TuneinFakeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("getRankedData")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Root))]
        public IActionResult Get()
        {
            var data = new Root()
            {
                Ranked = new List<Ranked>()
                {
                    new Ranked()
                    {
                        Priority = 2,
                        Vals = new Dictionary<string, object>()
                        {
                            { "timeout", "3s" },
                            { "num_threads", 500 },
                            { "buffer_size", 4000 },
                            { "use_sleep", true }
                        }
                    },
                    new Ranked()
                    {
                        Priority = 1,
                        Vals = new Dictionary<string, object>()
                        {
                            { "timeout", "2s" },
                            { "startup_delay", "2m" },
                            { "skip_percent_active", 0.2 }
                        }
                    },
                    new Ranked()
                    {
                        Priority = 0,
                        Vals = new Dictionary<string, object>()
                        {
                            { "num_threads", 300 },
                            { "buffer_size", 3000 },
                            { "label", "testing" }
                        }
                    }
                }
            };
            return Ok(data);
        }
    }
}
