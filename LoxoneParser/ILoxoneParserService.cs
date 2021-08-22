using LoxoneParser.Model;

namespace LoxoneParser
{
    public interface ILoxoneParserService
    {
        LoxoneConfig ParseLoxoneFile(string filepath);
        LoxoneRooms GetRoomsWithControls(LoxoneConfig loxoneConfig);
    }
}
