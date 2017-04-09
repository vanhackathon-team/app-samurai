using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces;
using HtmlAgilityPack;
using System.Linq;

namespace Robot.GooglePlay.SeachApp
{
    class SearchAppByLink : ISearchApp
    {
        private static HtmlNode[] getDescendents(HtmlNode cardDiv,
            string targetObject, string attribute, string comparationValue)
        {

            return cardDiv.Descendants(targetObject)
                    .Where(ts => ts.GetAttributeValue(attribute, string.Empty) == comparationValue)
                    .ToArray();
        }

        public IEnumerable<App> Search(string q, string country)
        {
            List<App> apps = new List<App>();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument html = web.Load(q);

            string divClass = "card-content id-track-click id-track-impression";
            var cardDivs = html.DocumentNode.Descendants("div")
                .Where(ts => ts.GetAttributeValue("class", string.Empty) == divClass);

            foreach (var cardDiv in cardDivs)
            {
                var names = getDescendents(cardDiv, "a", "class", "title");
                var subtitles = getDescendents(cardDiv, "a", "class", "subtitle");
                var prices = getDescendents(cardDiv, "span", "class", "display-price");
                var descriptions = getDescendents(cardDiv, "div", "class", "description");
                var ratings = getDescendents(cardDiv, "div", "class", "current-rating");

                for (int index = 0; index < names.Length; index++)
                {
                    string rating = ratings[index].GetAttributeValue("style", string.Empty);
                    if (rating == string.Empty)
                        rating = "";
                    else
                        rating = rating.Split(':')[1].Replace("%", "").Replace(";", "");

                    App app = new App()
                    {
                        Name = names[index].InnerText,
                        SubTitle = subtitles[index].InnerText,
                        Price = prices[index].InnerText,
                        Description = descriptions[index].InnerText,
                        Package = names[index].GetAttributeValue("href", string.Empty).Split('=')[1],
                        Rating = rating
                    };
                    apps.Add(app);
                }
            }
            return apps;
        }
    }
}
