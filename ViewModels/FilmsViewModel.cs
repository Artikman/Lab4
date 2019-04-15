using System;
using System.Linq;
using Lab_4.Models;

namespace Lab_4.ViewModels
{
    public class FilmsViewModel
    {
        public FilmView FilmViewModel { get; set; }
        public IQueryable<Film> PageViewModel { get; set; }
        public int PageNumber { get; set; }

        public class FilmView
        {
            public string Name { get; set; }
            public string Genre { get; set; }
            public string FilmCompany { get; set; }
            public DateTime? Duration { get; set; }
        }
    }
}