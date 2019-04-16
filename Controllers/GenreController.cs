using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Lab_4.Data;
using Lab_4.Models;
using Lab_4.ViewModels;
using Lab_4.Filters;
using System.Linq;

namespace Lab_4.Controllers
{
    [CatchExceptionFilter]
    public class GenreController : Controller
    {
        private int pageSize = 15;
        private CinemaContext db;
        private Genre _genre = new Genre
        {
            Name = ""
        };

        public GenreController(CinemaContext genreContext)
        {
            db = genreContext;
        }

        [HttpGet]
        [LoggingFilter]
        public IActionResult Index(SortState sortOrder)
        {
            Genre sessionGenre = (Genre)HttpContext.Session.GetObject("Genre");
            string sessionSortState = HttpContext.Session.GetString("SortStateGenre");
            int? page = HttpContext.Session.GetInt32("GenrePage");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("GenrePage", 0);
            }

            if(sessionGenre != null)
            {
                _genre = sessionGenre;
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            ViewData["NameSort"] = sortOrder == SortState.NameDesc ? SortState.NameAsc : SortState.NameDesc;
            HttpContext.Session.SetString("SortStateGenre", sortOrder.ToString());

            IQueryable<Genre> genres = Sort(db.Genres, sortOrder,
                _genre.Name, (int)page); 

            GenresViewModel genresView = new GenresViewModel
            {
                GenreViewModel = _genre,
                PageViewModel = genres,
                PageNumber = (int)page
            };

            return View(genresView);
        }

        [HttpPost]
        [LoggingFilter]
        public IActionResult Index(Genre genre)
        {
            var sessionSortState = HttpContext.Session.GetString("SortStateGenre");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            int? page = HttpContext.Session.GetInt32("GenrePage");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("GenrePage", 0);
            }

            IQueryable<Genre> genres = Sort(db.Genres, sortOrder,
                genre.Name, (int)page);
            HttpContext.Session.SetObject("Genre", genre);

            GenresViewModel genresView = new GenresViewModel
            {
                GenreViewModel = genre,
                PageViewModel = genres,
                PageNumber = (int)page
            };

            return View(genresView);
        }

        [SaveStateFilter("sort")]
        private IQueryable<Genre> Sort(IQueryable<Genre> genres,
            SortState sortOrder, string name, int page)
        {
            switch(sortOrder)
            {
                case SortState.NameAsc:
                    genres = genres.OrderBy(s => s.Name);
                    break;
                case SortState.NameDesc:
                    genres = genres.OrderByDescending(s => s.Name);
                    break;
            }
            genres = genres.Where(o => o.Name.Contains(name ?? ""))
                .Skip(page * pageSize).Take(pageSize);
            return genres;
        }
    }
}
