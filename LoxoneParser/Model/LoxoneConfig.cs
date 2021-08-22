using System.Collections.Generic;

namespace LoxoneParser.Model
{
    public class LoxoneConfig
    {
        public List<Page> Pages { get; set; } = new List<Page>();

        public List<Room> Rooms { get; set; } = new List<Room>();
    }
}
