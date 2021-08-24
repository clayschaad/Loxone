using LoxoneParser;
using LoxoneUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

        public LoxoneRoomController(ILogger<LoxoneRoomController> logger, ILoxoneParserService loxoneParserService)
        {
            this.logger = logger;
            this.loxoneParserService = loxoneParserService;
        }

        [HttpGet]
        public IEnumerable<LoxoneRoom> Get()
        {
            var loxoneConfig = loxoneParserService.ParseLoxoneFile(@"D:\Documents\Loxone\Loxone Config\Projects\Wohnung Schaad Bannau.Loxone");
            var x = loxoneParserService.GetRoomsWithControls(loxoneConfig);
            var rooms = x.Rooms.Select(r => new LoxoneRoom
            {
                Id = r.Id,
                Name = r.Name
            });
            return rooms;
        }
    }
}
