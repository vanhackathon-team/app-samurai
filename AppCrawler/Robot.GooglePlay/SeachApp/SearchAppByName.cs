using Domain.Interfaces;
using System.Collections.Generic;
using Domain.Entities;
using HtmlAgilityPack;
using Domain;
using System;
using Domain.SearchApp;

namespace Robot.GooglePlay.SeachApp
{
    class SearchAppByName : BaseSearchAppByName
    {
        public SearchAppByName(ISearchApp searchApp) : base(searchApp)
        {
        }

        protected override string GetSearchUrl(string q, string country)
        {
            return $"https://play.google.com/store/search?q={q}&hl={country}";            
        }
    }
}
