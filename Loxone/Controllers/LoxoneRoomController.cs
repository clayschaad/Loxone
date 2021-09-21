using LoxoneApi;
using LoxoneParser;
using LoxoneUI.Converter;
using LoxoneUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Loxone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoxoneRoomController : ControllerBase
    {
        private readonly ILogger<LoxoneRoomController> logger;
        private readonly IConfiguration configuration;
        private readonly ILoxoneParserService loxoneParserService;
        private readonly ILoxoneApiService loxoneApiService;

        public LoxoneRoomController(ILogger<LoxoneRoomController> logger, IConfiguration configuration, ILoxoneParserService loxoneParserService, ILoxoneApiService loxoneApiService)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.loxoneParserService = loxoneParserService;
            this.loxoneApiService = loxoneApiService;
        }

        [HttpGet]
        public LoxoneRooms Get()
        {
            var file = ReadLoxoneOptions().ConfigFile;
            var loxoneConfig = loxoneParserService.ParseLoxoneFile(file);
            var rooms = LoxoneUiConverter.GetRoomsWithControls(loxoneConfig);
            return rooms;
        }

        [HttpPost]
        [Route("light")]
        public IActionResult ControlLight([FromBody] LightData data)
        {
            loxoneApiService.SetLight(ReadLoxoneOptions(), data.Id, data.SceneId);
            return Ok();
        }

        [HttpPost]
        [Route("jalousie")]
        public IActionResult ControlJalousie([FromBody] JalousieData data)
        {
            loxoneApiService.SetJalousie(ReadLoxoneOptions(), data.Id, data.Direction);
            return Ok();
        }

        private LoxoneOptions ReadLoxoneOptions()
        {
            var options = new LoxoneOptions();
            configuration.GetSection(LoxoneOptions.Loxone).Bind(options);
            return options;
        }
    }
}
