using LoxoneApi;
using LoxoneParser;
using LoxoneUI.Converter;
using LoxoneUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Loxone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoxoneRoomController : ControllerBase
    {
        private readonly ILogger<LoxoneRoomController> logger;
        private readonly ILoxoneParserService loxoneParserService;
        private readonly ILoxoneApiService loxoneApiService;

        public LoxoneRoomController(ILogger<LoxoneRoomController> logger, ILoxoneParserService loxoneParserService, ILoxoneApiService loxoneApiService)
        {
            this.logger = logger;
            this.loxoneParserService = loxoneParserService;
            this.loxoneApiService = loxoneApiService;
        }

        [HttpGet]
        public LoxoneRooms Get()
        {
            var loxoneConfig = loxoneParserService.ParseLoxoneFile(@"D:\Documents\Loxone\Loxone Config\Projects\Wohnung Schaad Bannau.Loxone");
            var rooms = LoxoneUiConverter.GetRoomsWithControls(loxoneConfig);
            return rooms;
        }

        [HttpPost]
        [Route("light")]
        public IActionResult ControlLight([FromBody] LightData data)
        {
            loxoneApiService.SetLight(data.Id, data.SceneId);
            return Ok();
        }

        [HttpPost]
        [Route("jalousie")]
        public IActionResult ControlJalousie([FromBody] JalousieData data)
        {
            loxoneApiService.SetJalousie(data.Id, data.Direction);
            return Ok();
        }
    }
}
