using LoxoneParser.Model;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace LoxoneParser
{
    public class LoxoneParserService : ILoxoneParserService
    {
        public LoxoneConfig ParseLoxoneFile(string filepath)
        {
            var loxoneConfig = Parse(filepath);
            return loxoneConfig;
        }

        public LoxoneRooms GetRoomsWithControls(LoxoneConfig loxoneConfig)
        {
            var loxoneRooms = new LoxoneRooms();
            foreach (var room in loxoneConfig.Rooms)
            {
                var controls = loxoneConfig.Pages.SelectMany(p => p.Controls).Where(c => c.Room.Id == room.Id);
                if (controls.Any())
                {
                    var roomWithControls = new RoomWithControls();
                    roomWithControls.Id = room.Id;
                    roomWithControls.Title = room.Title;
                    roomWithControls.Controls = controls.ToList();
                    loxoneRooms.Rooms.Add(roomWithControls);
                }
            }

            return loxoneRooms;
        }

        private LoxoneConfig Parse(string filepath)
        {
            var doc = new XmlDocument();
            doc.Load(filepath);

            var loxoneConfig = new LoxoneConfig();

            // Rooms
            foreach (XmlNode node in doc.SelectNodes("//C[@Type='PlaceCaption']/C"))
            {
                var room = new Room();
                room.Id = node.Attributes["U"].Value;
                room.Title = node.Attributes["Title"].Value;
                loxoneConfig.Rooms.Add(room);
            }

            // Program pages
            foreach (XmlNode node in doc.SelectNodes("//C[@Type='Program']/C"))
            {
                var page = new Page();
                page.Title = node.Attributes["Title"].Value;

                // Light
                var lightNode = node.SelectSingleNode("C[@Type='LightController2']");
                if (lightNode != null)
                {
                    var control = new LightControl();
                    page.Controls.Add(control);

                    control.Id = lightNode.Attributes["U"].Value;
                    control.Title = lightNode.Attributes["Title"].Value;
                    control.Room = ParseRoom(lightNode, loxoneConfig.Rooms);

                    // Light Scenes
                    var lightScenes = lightNode.SelectNodes("LightscenesC/LightsceneC");
                    foreach (XmlNode lightSceneNode in lightScenes)
                    {
                        var lightScene = new LightScene();
                        control.LightScenes.Add(lightScene);
                        lightScene.Name = lightSceneNode.Attributes["Name"].Value;
                        lightScene.Id = control.LightScenes.Count();
                    }
                }

                // Jalousie
                var jalousieNodes = node.SelectNodes("C[@Type='AutoJalousie']");
                foreach (XmlNode jalousieNode in jalousieNodes)
                {
                    if (jalousieNode != null)
                    {
                        var control = new JalousieControl();
                        page.Controls.Add(control);

                        control.Id = jalousieNode.Attributes["U"].Value;
                        control.Title = jalousieNode.Attributes["Title"].Value;
                        control.Room = ParseRoom(jalousieNode, loxoneConfig.Rooms);
                    }
                }

                loxoneConfig.Pages.Add(page);
            }

            return loxoneConfig;
        }

        private Room ParseRoom(XmlNode node, List<Room> rooms)
        {
            var ioData = node.SelectSingleNode("IoData");
            var roomId = ioData.Attributes["Pr"].Value;
            return rooms.Single(r => r.Id == roomId);
        }
    }
}
