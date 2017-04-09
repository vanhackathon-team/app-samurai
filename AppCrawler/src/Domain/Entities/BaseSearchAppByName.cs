using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Domain
{
    public abstract class BaseSearchAppByName : ISearchApp
    {
        public readonly ISearchApp SearchApp;

        public BaseSearchAppByName(ISearchApp searchApp)
        {
            SearchApp = searchApp;
        }

        protected abstract string getSearchUrl(string q, string country);

        public IEnumerable<App> Search(string q, string country)
        {
            if (IsNotALink(q))
                q = getSearchUrl(q, country);

            return SearchApp?.Search(q, country);
        }

        private bool IsNotALink(string q)
        {
            return Uri.IsWellFormedUriString(q, UriKind.Absolute) == false;
        }
                
    }
}
