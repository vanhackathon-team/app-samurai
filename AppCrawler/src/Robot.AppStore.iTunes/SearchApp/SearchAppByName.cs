using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces;
using Domain;

namespace Robot.AppStore.iTunes.SearchApp
{
    public class SearchAppByName : BaseSearchAppByName
    {
        public SearchAppByName(ISearchApp searchApp) : base(searchApp)
        {            
        }

        public IEnumerable<App> Search(string q, string country)
        {   if (IsNotALink(q))
                q = $"http://www.apple.com/{country}/search/{q}?src=serp";

            return SearchApp?.Search(q, country);
        }

        private bool IsNotALink(string q)
        {
            return $"http://www.apple.com/{country}/search/{q}?src=serp";
        }
    }
}
