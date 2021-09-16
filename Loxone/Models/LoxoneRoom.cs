using System;
using System.Collections.Generic;

namespace LoxoneUI.Models
{
    public class LoxoneRoom
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<Control> Controls { get; set; }
    }

    public class Control
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
