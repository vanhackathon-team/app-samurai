using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using HtmlAgilityPack;
using Robot.GooglePlay.Helpers;

namespace Robot.GooglePlay.GetApp
{
    public class GetApp : IGetApp
    {
        public App Get(string id, string country)
        {
            HtmlWeb web = new HtmlWeb();

            string url = $"https://play.google.com/store/apps/details?id={id}&gl={country}";
            HtmlDocument html = web.Load(url);
            
            string divClass = "details-wrapper apps square-cover id-track-partial-impression id-deep-link-item";
            var divs = GetNode(html.DocumentNode, "div", "class", divClass);


            App app = new App()
            {
                Package = id,
                Name = GetNode(divs, "div", "class", "id-app-title").InnerText,
                SubTitle = GetNode(divs, "span", "itemprop", "name").InnerText,
                Description = GetNode(divs, "div", "jsname", "C4s9Ed").InnerText,
                Link = url.Split('&')[0],
                
                Icon = GetNode(divs, "img", "class", "cover-image")
                    .GetAttributeValue("src", string.Empty),   
                
                Category = GetNode(divs, "a", "class", "document-subtitle category")
                    .GetAttributeValue("href", string.Empty).Split('/')[4],
                
                Screenshots = GetNode(divs, "div", "class", "thumbnails")
                    .Descendants("img")
                    .Select(ts => ts.GetAttributeValue("src", string.Empty))
                    .ToArray()
                    
            };
            
            app.Rating = GooglePlayUtils.GetRating(
                GetNode(divs, "div", "class", "tiny-star star-rating-non-editable-container")
                .Descendants("div")
                .FirstOrDefault(ts => ts.GetAttributeValue("class", string.Empty) == "current-rating")
                .GetAttributeValue("style", string.Empty)
            );
            app.RankingCategory = GetRankingPosition(app.Category, id);
            app.PositionOverall = GetRankingPosition(app.Category, id, country);

            return app;
        }

        private static HtmlNode GetNode(HtmlNode node,
           string targetObject, string attribute, string comparationValue)
        {
            return node.Descendants(targetObject)
                .FirstOrDefault(ts => ts.GetAttributeValue(attribute, string.Empty) == comparationValue);
        }

        public int GetRankingPosition(string category, string appId)
        {
            string url = $"https://play.google.com/store/apps/category/{category}/collection/topselling_free";
            return MapHtmlRankingPosition(url, appId);
        }

        public int GetRankingPosition(string category, string appId, string country)
        {
            string url = $"https://play.google.com/store/apps/category/{category}/collection/topselling_free?gl={country}";
            return MapHtmlRankingPosition(url, appId);
        }

        public int MapHtmlRankingPosition(string url, string appId)
        {
            HtmlWeb web = new HtmlWeb();            
            HtmlDocument html = web.Load(url);

            var node = html.DocumentNode.Descendants("a")
                .Where(ts => ts.GetAttributeValue("class", string.Empty) == "title")
                .FirstOrDefault(ts => ts.GetAttributeValue("href", string.Empty)
                .Contains(appId));

            return node == null ? 0 : Convert.ToInt16(node.InnerText.Split('.')[0]);
        }
    }
}
