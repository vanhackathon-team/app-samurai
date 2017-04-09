using Domain;
using Domain.Entities;
using Domain.Interfaces;
using Robot.GooglePlay.SearchApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.GooglePlay
{
    class Program
    {
        public static void Main(string[] args)
        {
            ISearchApp search = new SearchAppByName(new SearchAppByLink(null));             
        }
    }
}
