using LoxoneParser.Model;

namespace LoxoneParser
{
    public interface ILoxoneParserService
    {
        LoxoneConfig ParseLoxoneFile(string filepath);
    }
}
