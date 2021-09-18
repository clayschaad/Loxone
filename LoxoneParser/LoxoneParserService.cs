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
                room.Name = node.Attributes["Title"].Value;
                loxoneConfig.Rooms.Add(room);
            }

            // Categories
            foreach (XmlNode node in doc.SelectNodes("//C[@Type='CategoryCaption']/C"))
            {
                var category = new Category();
                category.Id = node.Attributes["U"].Value;
                category.Name = node.Attributes["Title"].Value;
                loxoneConfig.Categories.Add(category);
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
                    control.Name = lightNode.Attributes["Title"].Value;
                    control.RoomId = GetRoomId(lightNode, loxoneConfig.Rooms);
                    control.CategoryId = GetCategoryId(lightNode, loxoneConfig.Categories);

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
                        control.Name = jalousieNode.Attributes["Title"].Value;
                        control.RoomId = GetRoomId(jalousieNode, loxoneConfig.Rooms);
                        control.CategoryId = GetCategoryId(jalousieNode, loxoneConfig.Categories);
                    }
                }

                loxoneConfig.Pages.Add(page);
            }

            return loxoneConfig;
        }

        private LoxoneId GetRoomId(XmlNode node, List<Room> rooms)
        {
            var ioData = node.SelectSingleNode("IoData");
            var roomId = ioData.Attributes["Pr"].Value;
            if (rooms.Exists(r => r.Id == roomId))
            {
                return roomId;
            }

            throw new KeyNotFoundException($"Room {roomId} does not exist");
        }

        private LoxoneId GetCategoryId(XmlNode node, List<Category> categories)
        {
            var ioData = node.SelectSingleNode("IoData");
            var categoryId = ioData.Attributes["Cr"].Value;
            if (categories.Exists(r => r.Id == categoryId))
            {
                return categoryId;
            }

            throw new KeyNotFoundException($"Category {categoryId} does not exist");
        }
    }
}
