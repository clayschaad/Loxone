using System.Collections.Generic;

namespace LoxoneParser.Model
{
    public class LightControl : Control
    {
        public List<LightScene> LightScenes { get; set; } = new List<LightScene>();
    }

    public class LightScene
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
