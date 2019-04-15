using System.Linq;
using Lab_4.Models;

namespace Lab_4.ViewModels
{

    public enum SortState
    {
        No,
        NameAsc,
        NameDesc,
        GenreDesc,
        GenreAsc,
        DurationAsc,
        DurationDesc,
        SessionAsc,
        SessionDesc,
        PlaceNumberAsc,
        PlaceNumberDesc,
        EmployessInvolvedInTheSessionAsc,
        EmployessInvolvedInTheSessionDesc,
    }

    public class GenresViewModel
    {
        public Genre GenreViewModel { get; set; }
        public IQueryable<Genre> PageViewModel { get; set; }
        public int PageNumber { get; set; }
    }
}