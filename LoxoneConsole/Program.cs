using LoxoneApi;
using LoxoneParser;
using LoxoneParser.Model;
using System;
using System.Linq;
using System.Threading;

namespace LoxoneConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Loxone!");

            var loxoneParserService = new LoxoneParserService();
            var loxoneApiService = new LoxoneApiService();

            var loxoneConfig = loxoneParserService.ParseLoxoneFile(@"D:\Documents\Loxone\Loxone Config\Projects\Wohnung Schaad Bannau.Loxone");
            var rooms = loxoneParserService.GetRoomsWithControls(loxoneConfig);

            var buero = rooms.Rooms.Single(r => r.Title == "Büro");
            var light = (LightControl)buero.Controls.Single(b => b is LightControl);
            var bueroEin = light.LightScenes.Single(l => l.Name == "Viel Licht");
            var bueroAus = light.LightScenes.Single(l => l.Name == "Aus");

            Console.WriteLine($"Setze Scene {bueroEin.Name} in {buero.Title}");
            loxoneApiService.SetLight(light.Id, bueroEin.Id).Wait();

            Thread.Sleep(2000);

            Console.WriteLine($"Setze Scene {bueroAus.Name} in {buero.Title}");
            loxoneApiService.SetLight(light.Id, bueroAus.Id).Wait();
        }
    }
}
