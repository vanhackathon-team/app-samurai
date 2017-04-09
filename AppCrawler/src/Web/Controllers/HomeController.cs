using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Robot.AppStore.iTunes.GetApp;
using Robot.AppStore.iTunes.SearchApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {


            return View();
        }
    }
}
