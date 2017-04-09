using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces;
using Domain.SearchApp;

namespace Robot.AppStore.iTunes.SearchApp
{
    public class SearchAppByName : BaseSearchAppByName
    {
        public SearchAppByName(ISearchApp searchApp) : base(searchApp)
        {            
        }
        protected override string GetSearchUrl(string q, string country)
        {
            return $"http://www.apple.com/{country}/search/{q}?src=serp";
        }
    }
}
