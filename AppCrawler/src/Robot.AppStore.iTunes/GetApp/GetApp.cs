using System;
using Domain.Entities;
using Domain.Interfaces;
using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;

namespace Robot.AppStore.iTunes.GetApp
{
    public class GetApp : IGetApp
    {
        public App Get(string urlApp, string country)
        {
            HtmlWeb web = new HtmlWeb();  
            HtmlDocument doc = web.Load(urlApp);

            var containerResult = doc.DocumentNode
                .Descendants("div")
                .FirstOrDefault(cr => cr.Id == "content");

            if (containerResult != null)
            {
                string appName = containerResult
                    .Descendants("h1")
                    .FirstOrDefault(a => a.GetAttributeValue("itemprop", string.Empty) == "name")
                    .InnerText;

                string appDescription = containerResult
                    .Descendants("div")
                    .FirstOrDefault(a => a.GetAttributeValue("class", string.Empty)
                    .Contains("product-review"))
                    .InnerText;

                string appImageLink = containerResult
                    .Descendants("div")
                    .FirstOrDefault(a => a.GetAttributeValue("class", string.Empty)
                    .Contains("artwork"))
                    .GetAttributeValue("src", string.Empty);

                string[] screenshots = containerResult
                    .Descendants("div")
                    .FirstOrDefault(s => s.GetAttributeValue("class", string.Empty).Contains("image-wrapper"))
                    .Descendants("img")
                    .Select(i => i.GetAttributeValue("src", string.Empty)).ToArray();

                App app = new App()
                {
                    Name = appName,
                    Description = appDescription,
                    Icon = appImageLink,
                    Link = urlApp,
                    Screenshots = screenshots
                };
            }
            return null;
        }
        
    }
}
