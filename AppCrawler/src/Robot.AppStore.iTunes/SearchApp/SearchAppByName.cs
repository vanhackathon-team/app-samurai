using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces;

namespace Robot.AppStore.iTunes.SearchApp
{
    public class SearchAppByName : ISearchApp
    {
        public readonly ISearchApp SearchApp;

        public SearchAppByName(ISearchApp searchApp)
        {
            SearchApp = searchApp;
        }

        public IEnumerable<App> Search(string q, string country)
        {
            if (IsNotALink(q))
                q = $"http://www.apple.com/{country}/search/{q}?src=serp";

            return SearchApp?.Search(q, country);
        }

        private bool IsNotALink(string q)
        {
            return Uri.IsWellFormedUriString(q, UriKind.Absolute) == false;
        }
    }
}
