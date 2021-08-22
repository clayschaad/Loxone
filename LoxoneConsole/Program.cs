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

            var lightBuero = (LightControl)loxoneConfig.Pages.SelectMany(s => s.Controls).Single(t => t is LightControl && t.Room.Title == "Büro");
            var bueroEin = lightBuero.LightScenes.Single(l => l.Name == "Viel Licht");
            var bueroAus = lightBuero.LightScenes.Single(l => l.Name == "Aus");

            Console.WriteLine($"Setze Scene {bueroEin.Name} in {lightBuero.Room.Title}");
            loxoneApiService.SetLight(lightBuero.Id, bueroEin.Id).Wait();

            Thread.Sleep(2000);

            Console.WriteLine($"Setze Scene {bueroAus.Name} in {lightBuero.Room.Title}");
            loxoneApiService.SetLight(lightBuero.Id, bueroAus.Id).Wait();
        }
    }
}
