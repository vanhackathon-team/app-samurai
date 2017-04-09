using Domain.Entities;
using Domain.Interfaces;
using HtmlAgilityPack;
using Robot.AppStore.iTunes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Robot.AppStore.iTunes.SearchApp
{
    public class SearchAppByLink : ISearchApp
    {
        public ISearchApp SearchApp { get; }

        public SearchAppByLink(ISearchApp searchApp)
        {
            SearchApp = searchApp;
        }

        public IEnumerable<App> Search(string q, string country)
        {
            if (IsALink(q) == false)
                return SearchApp?.Search(q, country);
            
            q = AppUrl.GetUrlWithCountry("http://www.apple.com", q, country);
            if (q == null)
                return new List<App>();

            return MapHtmlToApps(q);
        }

        private IEnumerable<App> MapHtmlToApps(string appUrl)
        {
            IList<App> apps = new List<App>();

            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(appUrl);

            var containerResult = doc.DocumentNode.Descendants("div")
                .FirstOrDefault(eu => eu.GetAttributeValue("id", string.Empty) == "exploreCurated");

            if (containerResult != null)
            {
                var results = containerResult.Descendants("div")
                            .Where(a => a.GetAttributeValue("class", string.Empty)
                            .Contains("as-explore-product"));

                foreach (var app in results)
                {
                    string appName = app.Descendants("h2")
                                        .FirstOrDefault(n => n.GetAttributeValue("class", string.Empty)
                                        .Contains("as-productname")).InnerHtml;

                    string appPartialDescription = app.Descendants("p")
                                        .FirstOrDefault(n => n.GetAttributeValue("class", string.Empty)
                                        .Contains("as-productdescription")).InnerHtml;

                    string appImage = app.Descendants("img")
                                        .FirstOrDefault(n => n.GetAttributeValue("class", string.Empty)
                                        .Contains("as-explore-img"))
                                        .GetAttributeValue("src", string.Empty);

                    string appLink = app.Descendants("a")
                                        .FirstOrDefault(n => n.GetAttributeValue("class", string.Empty)
                                        .Contains("as-links-name"))
                                        .GetAttributeValue("href", string.Empty);

                    var appResult = new App()
                    {
                        Name = appName,
                        Description = appPartialDescription,
                        Icon = appImage,
                        Link = appLink,
                        Package = GetApp.GetApp.ExtractIdFromAppUrl(appLink)
                    };

                    apps.Add(appResult);
                }
            }

            return apps;
        }

        private bool IsALink(string q)
        {
            return Uri.IsWellFormedUriString(q, UriKind.Absolute);
        }
    }
}
