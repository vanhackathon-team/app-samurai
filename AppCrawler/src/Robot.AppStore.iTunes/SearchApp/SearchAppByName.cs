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

        public IEnumerable<App> Search(string q)
        {
            // TODO: Implent search when parameter is a name 
            if (IsNotALink(q) == false)
                return SearchApp?.Search(q);

            return null;
        }

        private bool IsNotALink(string q)
        {
            return Uri.IsWellFormedUriString(q, UriKind.Absolute) == false;
        }
    }
}
