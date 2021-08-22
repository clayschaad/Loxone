namespace LoxoneParser.Model
{
    [System.Diagnostics.DebuggerDisplay("{Id}:{Name}")]
    public class Category
    {
        public LoxoneId Id { get; set; }
        public string Name { get; set; }
    }
}
