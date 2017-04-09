using Domain.Interfaces;
using System.Collections.Generic;
using Domain.Entities;
using HtmlAgilityPack;
using Domain;
using System;
using Domain.SearchApp;

namespace Robot.GooglePlay.SearchApp
{
    public class SearchAppByName : BaseSearchAppByName
    {
        public SearchAppByName(ISearchApp searchApp) : base(searchApp)
        {
        }

        protected override string GetSearchUrl(string q, string country)
        {
            return $"https://play.google.com/store/search?q={q}&gl={country}&c=apps";            
        }
    }
}
