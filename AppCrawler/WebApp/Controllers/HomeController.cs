using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Robot;
using Domain.Entities;
using Domain.Helpers;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index(string p, string q, string c)
        {
            if (q == string.Empty || (p != "g" && p != "a"))
                return View(null);

            ISearchApp searchApp;

            if (p == "g")
                searchApp = new Robot
                    .GooglePlay.SearchApp.SearchAppByName
                    (new Robot.GooglePlay.SearchApp.SearchAppByLink(null));
            else
                searchApp = new Robot
                    .AppStore.iTunes.SearchApp.SearchAppByName
                    (new Robot.AppStore.iTunes.SearchApp.SearchAppByLink(null));

            IEnumerable<App> searchResult = searchApp.Search(Utils.FixSeachLink(q), c);

            ViewBag.p = p;
            ViewBag.q = q;
            ViewBag.c = c;

            return View(searchResult);
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

        [HttpGet]
        public IActionResult Details(string p, string q, string c)
        {
            if(q == string.Empty || ( p != "g" && p != "a"))
                return RedirectToAction("Index","Home");

            IGetApp getApp;

            if (p == "g")
                getApp = new Robot.GooglePlay.GetApp.GetApp();
            else
                getApp = new Robot.AppStore.iTunes.GetApp.GetApp();

            var app = getApp.Get(q, c);

            ViewBag.p = p;
            ViewBag.q = q;
            ViewBag.c = c;
            
            return View(app);
        }
    }
}
