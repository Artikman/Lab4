using System.Linq;
using Lab_4.Models;

namespace Lab_4.ViewModels
{
    public class PlacesViewModel
    {
        public Place PlaceViewModel { get; set; }
        public IQueryable<Place> PageViewModel { get; set; }
        public int PageNumber { get; set; }
    }
}