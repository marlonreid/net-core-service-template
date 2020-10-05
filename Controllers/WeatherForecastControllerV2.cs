using System;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreServiceTemplate.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WeatherForecastV2Controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public string Get()
        {
            var rng = new Random();
            return Summaries[rng.Next(0, 9)];
        }

        [HttpDelete]
        [Obsolete]
        public int Delete()
        {
            return 0;
        }
    }
}
