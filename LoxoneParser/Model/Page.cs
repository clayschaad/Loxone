using System.Collections.Generic;

namespace LoxoneParser.Model
{
    public class Page
    {
        public string Title { get; set; }
        public List<Control> Controls { get; set; } = new List<Control>();
    }
}
