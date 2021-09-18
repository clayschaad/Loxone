using System.Linq;

namespace LoxoneUI.Converter
{
    public static class LoxoneUiConverter
    {
        public static LoxoneRooms GetRoomsWithControls(LoxoneParser.Model.LoxoneConfig loxoneConfig)
        {
            var loxoneRooms = new LoxoneRooms();
            foreach (var room in loxoneConfig.Rooms.OrderBy(r => r.Name))
            {
                var controls = loxoneConfig.Pages.SelectMany(p => p.Controls).Where(c => c.RoomId == room.Id);
                if (controls.Any())
                {
                    var roomWithControls = new RoomWithControls();
                    roomWithControls.Id = room.Id;
                    roomWithControls.Name = room.Name;
                    roomWithControls.JalousieControls = controls.Where(c => c.CategoryId == LoxoneConstants.JalousieCategory).OrderBy(c => c.Name).Select(CreateJalousieControl).ToList();
                    roomWithControls.LightControls = controls.Where(c => c.CategoryId == LoxoneConstants.LightCategory).OrderBy(c => c.Name).Select(CreateLightControl).ToList();
                    loxoneRooms.Rooms.Add(roomWithControls);
                }
            }

            return loxoneRooms;

            JalousieControl CreateJalousieControl(LoxoneParser.Model.Control c)
            {
                return new JalousieControl
                {
                    Id = c.Id,
                    Name = c.Name,
                    RoomId = c.RoomId,
                    CategoryId = c.CategoryId
                };
            }

            LightControl CreateLightControl(LoxoneParser.Model.Control c)
            {
                return new LightControl
                {
                    Id = c.Id,
                    Name = c.Name,
                    RoomId = c.RoomId,
                    CategoryId = c.CategoryId,
                    LightScenes = ((LoxoneParser.Model.LightControl)c).LightScenes.OrderBy(l => l.Name).Select(CreateLightScene).ToList()
                };
            }

            LightScene CreateLightScene(LoxoneParser.Model.LightScene ls)
            {
                return new LightScene
                {
                    Id = ls.Id,
                    Name = ls.Name
                };
            }
        }
    }
}
