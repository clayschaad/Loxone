using LoxoneParser.Model;
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

                    // Light Scenes LightscenesC
                    var lightScenes = lightNode.SelectNodes("LightscenesC/LightsceneC");
                    foreach (XmlNode lightSceneNode in lightScenes)
                    {
                        var lightScene = new LightScene();
                        control.LightScenes.Add(lightScene);
                        lightScene.Name = lightSceneNode.Attributes["Name"].Value;
                        lightScene.Id = control.LightScenes.Count();
                    }

                    // Room
                    var ioData = lightNode.SelectSingleNode("IoData");
                    var roomId = ioData.Attributes["Pr"].Value;
                    control.Room = loxoneConfig.Rooms.Single(r => r.Id == roomId);
                    
                }

                // Jalousie
                var storeNodes = node.SelectNodes("C[@Type='AutoJalousie']");
                foreach (XmlNode storeNode in storeNodes)
                {
                    if (storeNode != null)
                    {
                        var control = new JalousieControl();
                        control.Id = storeNode.Attributes["U"].Value;
                        control.Title = storeNode.Attributes["Title"].Value;
                        page.Controls.Add(control);
                    }
                }

                loxoneConfig.Pages.Add(page);
            }

            return loxoneConfig;
        }
    }
}
