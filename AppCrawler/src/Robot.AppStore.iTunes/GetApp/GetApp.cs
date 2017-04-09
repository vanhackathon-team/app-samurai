using Domain.Entities;
using Domain.Interfaces;
using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
using Robot.AppStore.iTunes.Helpers;

namespace Robot.AppStore.iTunes.GetApp
{
    public class GetApp : IGetApp
    {
        public App Get(string urlApp, string country)
        {
            HtmlWeb web = new HtmlWeb();

            string urlAppWithCountry = AppUrl.GetUrlWithCountry("https://itunes.apple.com", urlApp, country);
            HtmlDocument doc = web.Load(urlAppWithCountry);

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
                    .Descendants("img")
                    .FirstOrDefault()
                    .GetAttributeValue("src", string.Empty);

                string[] screenshots = containerResult
                    .Descendants("div")
                    .FirstOrDefault(s => s.GetAttributeValue("class", string.Empty).Contains("screenshots"))
                    .Descendants("img")
                    .Select(i => i.GetAttributeValue("src", string.Empty)).ToArray();

                var categoryContainer = containerResult.Descendants("div")
                    .FirstOrDefault(a => a.Id == "left-stack")
                        .Descendants("li")
                        .FirstOrDefault(l => l.GetAttributeValue("class", string.Empty)
                            .Contains("genre"))
                            .Descendants("a")
                            .FirstOrDefault();

                string categoryName = categoryContainer
                                        .Descendants("span")                     
                                        .FirstOrDefault()
                                        .InnerHtml;

                string categoryLink = categoryContainer
                            .GetAttributeValue("href", string.Empty);

                App app = new App()
                {
                    Name = appName,
                    Description = appDescription,
                    Icon = appImageLink,
                    Link = urlApp,
                    Screenshots = screenshots,
                    RankingCategory = GetRankingCategory(categoryLink, urlApp),
                    PositionOverall = FillPositionOverall(urlApp),
                    Category = categoryName
                };

                return app;
            }

            return null;
        }

        private int GetRankingCategory(string urlCategory, string urlApp)
        {
            HtmlWeb web = new HtmlWeb();

            var docCategory = web.Load(urlCategory);

            var containerAppsCategory = docCategory.DocumentNode
                                            .Descendants("div")
                                            .FirstOrDefault(d => d.Id == "selectedcontent");

            var categoryAppsLinks = containerAppsCategory
                                        .Descendants("a")
                                            .Select(a => a.GetAttributeValue("href", string.Empty));

            int positionAppCategory = -1;
            for (int i = 0; i < categoryAppsLinks.Count(); i++)
            {
                var l = categoryAppsLinks.ElementAt(i);

                if (l == urlApp)
                {
                    positionAppCategory = i + 1;
                    break;
                }
            }

            return positionAppCategory;
        }

        private int FillPositionOverall(string appUrl)
        {
            string urlOverall = $"http://www.apple.com/itunes/charts/free-apps/";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(urlOverall);

            var containerApps = doc.DocumentNode
                                    .Descendants("section")
                                    .FirstOrDefault(c => c.GetAttributeValue("class", string.Empty)
                                    .Contains("chart-grid"));

            var linksApps = containerApps
                                .Descendants("ul")
                                .FirstOrDefault()
                                .Descendants("a")
                                .Where(a => a.GetAttributeValue("class", string.Empty)
                                .Contains("more"))
                                .Select(u => u.GetAttributeValue("href", string.Empty));                                    

            var idCurrentApp = ExtractIdFromAppUrl(appUrl);

            int positionApp = -1;
            for (int i = 0; i < linksApps.Count(); i++)
            {
                var idApp = ExtractIdFromAppUrl(linksApps.ElementAt(i));
                if (idApp == idCurrentApp)
                {
                    positionApp = i + 1;
                    break;
                }
            }

            return positionApp;
        }

        public static string ExtractIdFromAppUrl(string appUrl)
        {
            var urlSplited = appUrl.Split('/');
            var idWithParametersUrl = urlSplited[urlSplited.Length - 1];

            var id = idWithParametersUrl.Split('?')[0];

            return id;
        }
    }
}
