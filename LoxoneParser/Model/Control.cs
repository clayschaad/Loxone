namespace LoxoneParser.Model
{
    [System.Diagnostics.DebuggerDisplay("{Id}:{Name}")]
    public class Control
    {
        public LoxoneId Id { get; set; }
        public string Name { get; set; }
        public LoxoneId RoomId { get; set; }
        public LoxoneId CategoryId { get; set; }
    }
}
