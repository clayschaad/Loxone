using System.Collections.Generic;

namespace LoxoneUI.Converter
{
    public class RoomWithControls : Room
    {
        public List<LightControl> LightControls { get; set; } = new List<LightControl>();
        public List<JalousieControl> JalousieControls { get; set; } = new List<JalousieControl>();
    }
}
