using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Lab_4.Models;
using Lab_4.ViewModels;
using Lab_4.Services;
using Lab_4.Filters;

namespace Lab_4.Controllers
{
    [CatchExceptionFilter]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            HomeViewModel homeViewModel = TakeLast.GetHomeViewModel();
            return View(homeViewModel);
        }

        public IActionResult ToError()
        {
            int[] x = new int[3];
            for (int i = 0; i < 10; i++) { x[i] = x[i] + 1; }
            return View("~/Views/Home/About.cshtml");
        }

        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult About3()
        {
            HomeViewModel homeViewModel = TakeLast.GetHomeViewModel();
            return View("~/Views/Home/About.cshtml", homeViewModel);
        }

        [HttpGet]
        public IActionResult Film()
        {
            if (HttpContext.Session.Keys.Contains("name"))
            {
                ViewBag.Name = HttpContext.Session.GetString("name");
            }
            if (HttpContext.Session.Keys.Contains("genre"))
            {
                ViewBag.Genre = HttpContext.Session.GetString("genre");
            }
            if (HttpContext.Session.Keys.Contains("filmCompany"))
            {
                ViewBag.FilmCompany = HttpContext.Session.GetString("filmCompany");
            }
            if (HttpContext.Session.Keys.Contains("producingCountry"))
            {
                ViewBag.ProducingCountry = HttpContext.Session.GetString("producingCountry");
            }          
            return View("~/Views/Film/Index.cshtml");
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Session()
        {
            return View("~/Views/Session/Index.cshtml");
        }

        [HttpGet]
        public IActionResult Genre()
        {
            string name = "", description = "";
            if(HttpContext.Request.Cookies.ContainsKey("name"))
            {
                name = HttpContext.Request.Cookies["name"];
            }
            if(HttpContext.Request.Cookies.ContainsKey("description"))
            {
                description = HttpContext.Request.Cookies["description"];
            }
            ViewBag.Name = name;
            ViewBag.Description = description;
            return View("~/Views/Genre/Index.cshtml");
        }

        [HttpPost]
        public string Film(string name, string genre,
            string filmCompany, string producingCountry)
        {
            HttpContext.Session.SetString("name", name);
            HttpContext.Session.SetString("genre", genre);
            HttpContext.Session.SetString("filmCompany", filmCompany);
            HttpContext.Session.SetString("producingCountry", producingCountry.ToString());
            return "Фильм " + name + "с жанром " + genre + "\n" +
                "с компанией " + filmCompany + " и страной " + producingCountry + " успешно добавлен";
        }

        [HttpPost]
        public string Session(string session)
        {
            return "Сеанс " + session + " успешно зарегистрирован";
        }

        [HttpPost]
        public string Genre(string name, string description)
        {
            HttpContext.Response.Cookies.Append("name", name);
            HttpContext.Response.Cookies.Append("description", description);

            return "Фильм " + name + " с описанием " + description + " добавлен";
        }

        [HttpPost]
        public string Place(string place)
        {
            return "Свободное место " + place + " успешно зарегистрировано";
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}