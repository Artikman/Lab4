using System.Collections.Generic;
using Lab_4.Models;

namespace Lab_4.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Film> Films { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
        public IEnumerable<Place> Places { get; set; }
    }
}