﻿namespace LoxoneParser.Model
{
    [System.Diagnostics.DebuggerDisplay("{Id}:{Name}")]
    public class Room
    {
        public LoxoneId Id { get; set; }
        public string Name { get; set; }
    }
}
