using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string search = "whatsapp";

            HtmlWeb web = new HtmlWeb();
            var doc = web.Load($"http://www.apple.com/br/search/{search}?src=serp");
            var containerResult = doc.DocumentNode.Descendants("div")
                .FirstOrDefault( eu => eu.Id == "exploreCurated");

            var appsDetailsLinks = containerResult.Descendants("a")        
                            .Where(a => a.GetAttributeValue("class", string.Empty).Contains("as-links-name"));

            foreach (var link in appsDetailsLinks)
            {
                string appLink = link.GetAttributeValue("href", string.Empty);
                Console.WriteLine($"Details app - {appLink}");

                var docapp = web.Load(appLink);

                var categoryLink = docapp.DocumentNode
                    .Descendants("div")
                    .FirstOrDefault(a => a.Id == "left-stack")
                        .Descendants("li")
                        .FirstOrDefault(l => l.GetAttributeValue("class", string.Empty)
                            .Contains("genre"))
                            .Descendants("a")
                            .FirstOrDefault()
                            .GetAttributeValue("href", string.Empty);

                Console.WriteLine($"Category app = {categoryLink}");


                var docCategory = web.Load(categoryLink);

                var containerAppsCategory = docCategory.DocumentNode
                                                .Descendants("div")
                                                .FirstOrDefault(d => d.Id == "selectedcontent");

                var categoryAppsLinks = containerAppsCategory
                                            .Descendants("a")
                                                .Select(a => a.GetAttributeValue("href", string.Empty));

                int positionAppCategory = 1;
                for (int i = 0; i <= categoryAppsLinks.Count(); i++)
                {
                    var l = categoryAppsLinks.ElementAt(i);

                    if (l == appLink)
                    {
                        positionAppCategory = i + 1;
                        break;
                    }
                }

                Console.WriteLine($"Category Ranking = {positionAppCategory}");
            }

            Console.ReadKey();
        }
    }
}
