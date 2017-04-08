using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using HtmlAgilityPack;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();
            var doc = web.LoadFromWebAsync($"http://www.apple.com/br/search/angry-birds?src=serp").Result;
            var links = doc.DocumentNode.Descendants("div")
                .Where( eu => eu.Id == "exploreCurated");

            string linkDoApp = "";

            foreach (var item in links)
            {
                var links2 = item.Descendants("a")        
                    .FirstOrDefault(a => a.GetAttributeValue("class", string.Empty).Contains("as-links-name"));
                
               
                linkDoApp = links2.GetAttributeValue("href", string.Empty);
                Console.WriteLine($"app - {linkDoApp}");
            }

            var docapp = web.LoadFromWebAsync(linkDoApp).Result;

            var linkCategoria = docapp.DocumentNode
                .Descendants("div")
                .FirstOrDefault(a => a.Id == "left-stack")
                    .Descendants("li")
                    .FirstOrDefault(l => l.GetAttributeValue("class", string.Empty)
                        .Contains("genre"))
                        .Descendants("a")
                        .FirstOrDefault()
                        .GetAttributeValue("href", string.Empty);

            Console.WriteLine($"categoria = {linkCategoria}");



            var docCategory = web.LoadFromWebAsync(linkCategoria).Result;


            var containerAppsCategory = docCategory.DocumentNode
                                            .Descendants("div")
                                            .FirstOrDefault(d => d.Id == "selectedcontent");

            var linkAppsCategory = containerAppsCategory
                                        .Descendants("a")
                                            .Select(a => a.GetAttributeValue("href", string.Empty));

            int positionAppCategory = 1;
            for (int i = 0; i <= linkAppsCategory.Count(); i++)
            {
                var l = linkAppsCategory.ElementAt(i);

                if (l == linkDoApp)
                {
                    positionAppCategory = i + 1;
                    break;
                }
            }

            Console.WriteLine($"Posição na category = {positionAppCategory}");

            Console.ReadKey();
        }
    }
}
