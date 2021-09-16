using LoxoneApi;
using LoxoneParser;
using LoxoneUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<LoxoneRoom> Get()
        {
            var loxoneConfig = loxoneParserService.ParseLoxoneFile(@"D:\Documents\Loxone\Loxone Config\Projects\Wohnung Schaad Bannau.Loxone");
            var x = loxoneParserService.GetRoomsWithControls(loxoneConfig);
            var rooms = x.Rooms.Select(r => new LoxoneRoom
            {
                Id = r.Id,
                Name = r.Name,
                Controls = r.Controls.Select(c => new Control
                {
                    Id = c.Id,
                    Name = c.Name,
                    Category = c.CategoryId
                }).ToList()
            });
            return rooms;
        }

        [HttpPost]
        [Route("light")]
        public IActionResult ControlLight([FromBody] LightControl data)
        {
            loxoneApiService.SetLight(data.Id, data.SceneId);
            return Ok();
        }

        [HttpPost]
        [Route("jalousie")]
        public IActionResult ControlJalousie([FromBody] JalousieControl data)
        {
            loxoneApiService.SetJalousie(data.Id, data.Direction);
            return Ok();
        }
    }
}
