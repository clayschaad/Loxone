using System.Collections.Generic;

namespace LoxoneParser.Model
{
    public class RoomWithControls : Room
    {
        public List<Control> Controls { get; set; } = new List<Control>();
    }
}
