using LoxoneParser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loxone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> logger;
        private readonly ILoxoneParserService loxoneParserService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ILoxoneParserService loxoneParserService)
        {
            this.logger = logger;
            this.loxoneParserService = loxoneParserService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            loxoneParserService.ParseLoxoneFile(@"D:\Documents\Loxone\Loxone Config\Projects\Wohnung Schaad Bannau.Loxone");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
