using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Robot.AppStore.iTunes.GetApp;
using Robot.AppStore.iTunes.SearchApp;
using Domain.Entities;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Details(string search)
        {
            ISearchApp searchApp = new SearchAppByName(new SearchAppByLink(null));
            IGetApp getApp = new GetApp();

            IEnumerable<App> searchResult = searchApp.Search(search, "us");

            return View(searchResult);
        }
    }
}
