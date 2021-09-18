namespace LoxoneUI.Converter
{
    [System.Diagnostics.DebuggerDisplay("{Id}:{Name}")]
    public class Control
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string RoomId { get; set; }
        public string CategoryId { get; set; }
    }
}
