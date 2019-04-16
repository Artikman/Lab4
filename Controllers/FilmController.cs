using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Lab_4.Data;
using Lab_4.Models;
using Lab_4.ViewModels;
using Lab_4.Filters;
using static Lab_4.ViewModels.FilmsViewModel;

namespace Lab_4.Controllers
{
    [CatchExceptionFilter]
    public class FilmController : Controller
    {
        private int pageSize = 15;
        private CinemaContext db;
        private FilmView _film = new FilmView
        {
            Name = "",
            Genre = "",
            FilmCompany = "",
            Duration = null
        };

        public FilmController(CinemaContext FilmContext)
        {
            db = FilmContext;
        }

        [HttpGet]
        [LoggingFilter]
        public IActionResult Index(SortState sortOrder)
        {
            FilmView sessionFilm = (FilmView)HttpContext.Session.GetObject("Film");
            string sessionSortState = HttpContext.Session.GetString("SortStateFilm");
            int? page = HttpContext.Session.GetInt32("FilmPage");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("FilmPage", 0);
            }

            if(sessionFilm != null)
            {
                _film = sessionFilm;
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            ViewData["NameSort"] = sortOrder == SortState.NameDesc ? SortState.NameAsc : SortState.NameDesc;
            ViewData["GenreSort"] = sortOrder == SortState.GenreDesc ? SortState.GenreAsc : SortState.GenreDesc;

            HttpContext.Session.SetString("SortState", sortOrder.ToString());
            IQueryable<Film> Films = Sort(db.Films, sortOrder,
                _film.Name, _film.Genre, _film.FilmCompany, (int)page);

            FilmsViewModel FilmsView = new FilmsViewModel
            {
                FilmViewModel = _film,
                PageViewModel = Films,
                PageNumber = (int)page
            };

            return View(FilmsView);
        }

        [HttpPost]
        [LoggingFilter]
        public IActionResult Index(FilmView film)
        {
            var sessionSortState = HttpContext.Session.GetString("SortStateFilm");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            int? page = HttpContext.Session.GetInt32("FilmPage");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("FilmPage", 0);
            }

            IQueryable<Film> films = Sort(db.Films, sortOrder,
                film.Name, film.Genre, film.FilmCompany, (int)page);
            HttpContext.Session.SetObject("Film", film);

            FilmsViewModel filmsView = new FilmsViewModel
            {
                FilmViewModel = film,
                PageViewModel = films,
                PageNumber = (int)page
            };

            return View(filmsView);
        }

        [SaveStateFilter("sort")]
        private IQueryable<Film> Sort(IQueryable<Film> films,
            SortState sortOrder, string name, string genre, string filmcompany, int page)
        {
            switch (sortOrder)
            {
                case SortState.DurationAsc:
                    films = films.OrderBy(s => s.Duration);
                    break;
                case SortState.DurationDesc:
                    films = films.OrderByDescending(s => s.Name);
                    break;
            }
            films = films.Include(o => o.Genre).Include(o => o.Name)
                .Include(o => o.FilmCompany).Where(o => o.Name.Contains(name ?? ""))
                .Where(o => o.Genre.Contains(genre ?? ""))
                .Where(o => o.FilmCompany.Contains(filmcompany ?? ""))
                .Skip(page * pageSize).Take(pageSize);
            return films;
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<Film> films = FilmContext.GetPage(0, pageSize);
            return View(films);
        }

        [HttpPost]
        public string Add(string name, string genre)
        {
            return "Фильм " + name + "с жанром " + genre
                + " успешно зарегистрирован";
        }
    }
}
