using Domain.Entities;
using Domain.Interfaces;
using Robot.GooglePlay.SeachApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robot.GooglePlay;
using Robot.GooglePlay.GetApp;

namespace Test
{
    class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Starting ...\n");
            Console.WriteLine("============================================");

            /*ISearchApp robot = new SearchAppByName(new SearchAppByLink(null));

            IEnumerable<App> apps = robot.Search("whatsapp", "en");
            foreach (var app in apps)
                Console.WriteLine(app);
            */

            IGetApp robotApp = new GetApp();

            App app = robotApp.Get("com.whatsapp", "br");
            Console.WriteLine(app);

            foreach (string url in app.Screenshots)
                Console.WriteLine(url);

            Console.WriteLine("============================================");
            Console.WriteLine("\nEnd!");

            //com.whatsapp
            
            Console.ReadKey();
        }

        public static void Main2(string[] args)
        {
            /*ISearchApp searchApp = new SearchAppByName(new SearchAppByLink(null));
            IGetApp getApp = new GetApp();

            IEnumerable<App> searchResult = searchApp.Search("whatsapp", "us");

            foreach (var app in searchResult)
            {
                Console.WriteLine($"Name = {app.Name}");
                Console.WriteLine($"Description = {app.Description}");
                Console.WriteLine($"Icon = {app.Icon}");
                Console.WriteLine($"Link = {app.Link}");

                Console.WriteLine();               

                var appDetails = getApp.Get(app.Link, "us");

                Console.WriteLine($"Name = {appDetails.Name}");
                Console.WriteLine($"Description = {appDetails.Description}");
                Console.WriteLine($"Icon = {appDetails.Icon}");
                Console.WriteLine($"Link = {appDetails.Link}");
                Console.WriteLine($"Category Ranking = {(appDetails.RankingCategory == -1 ? "60+" : appDetails.RankingCategory.ToString())}");
                Console.WriteLine($"Category Overall = {(appDetails.PositionOverall == -1 ? "100+" : appDetails.PositionOverall.ToString())}");

                Console.WriteLine();
                Console.WriteLine("====================================================");
                Console.WriteLine();
            }

            Console.ReadKey();
            */
        }
    }
}
