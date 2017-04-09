using Domain.Interfaces;
using System.Collections.Generic;
using Domain.Entities;
using HtmlAgilityPack;
using Domain;
using System;

namespace Robot.GooglePlay.SeachApp
{
    class SearchAppByName : BaseSearchAppByName
    {
        public SearchAppByName(ISearchApp searchApp) : base(searchApp)
        {
        }

        protected override string getSearchUrl(string q, string country)
        {
            return $"https://play.google.com/store/search?q={a}&hl={country}";            
        }
    }
}
