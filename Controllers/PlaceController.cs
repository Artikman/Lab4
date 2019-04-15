using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Lab_4.Data;
using Lab_4.Models;
using Lab_4.ViewModels;
using Lab_4.Filters;

namespace Lab_4.Controllers
{
    [CatchExceptionFilter]
    public class PlaceController : Controller
    {
        private int pageSize = 15;
        private CinemaContext db;
        private Place _place = new Place
        {
            Session = "",
            PlaceNumber = 0
        };

        public PlaceController(CinemaContext PlaceContext)
        {
            db = PlaceContext;
        }

        [HttpGet]
        public IActionResult Index(SortState sortOrder)
        {
            Place sessionPlace = (Place)HttpContext.Session.GetObject("Place");
            string sessionSortState = HttpContext.Session.GetString("SortStatePlace");
            int? page = HttpContext.Session.GetInt32("PlacePage");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("PlacePage", 0);
            }

            if (sessionPlace != null)
            {
                _place = sessionPlace;   
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            ViewData["SessionSort"] = sortOrder == SortState.SessionDesc ? SortState.SessionAsc : SortState.SessionDesc;
            ViewData["PlaceNumberSort"] = sortOrder == SortState.PlaceNumberDesc ? SortState.PlaceNumberAsc : SortState.PlaceNumberDesc;

            HttpContext.Session.SetString("SortState", sortOrder.ToString());
            IQueryable<Place> Places = Sort(db.Places, sortOrder,
                _place.Session, (int)_place.PlaceNumber, (int)page);
            PlacesViewModel PlaceView = new PlacesViewModel
            {
                PlaceViewModel = _place,
                PageViewModel = Places,
                PageNumber = (int)page
            };

            return View(PlaceView);
        }

        [HttpPost]
        public IActionResult Index(Place place)
        {
            var sessionSortState = HttpContext.Session.GetString("SortStatePlace");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            int? page = HttpContext.Session.GetInt32("PlacePage");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("PlacePage", 0);
            }

            IQueryable<Place> Place = Sort(db.Places, sortOrder,
                place.Session, (int)place.PlaceNumber, (int)page);
            HttpContext.Session.SetObject("Place", place);

            PlacesViewModel PlacesView = new PlacesViewModel
            {
                PlaceViewModel = place,
                PageViewModel = Place,
                PageNumber = (int)page
            };

            return View(PlacesView);
        }

        private IQueryable<Place> Sort(IQueryable<Place> Places,
            SortState sortOrder, string session, int placeNumber, int page)
        {
            switch (sortOrder)
            {
                case SortState.SessionAsc:
                    Places = Places.OrderBy(s => s.Session);
                    break;
                case SortState.SessionDesc:
                    Places = Places.OrderByDescending(s => s.Session);
                    break;
                case SortState.PlaceNumberAsc:
                    Places = Places.OrderBy(s => s.PlaceNumber);
                    break;
                case SortState.PlaceNumberDesc:
                    Places = Places.OrderByDescending(s => s.PlaceNumber);
                    break;
            }
            Places = Places.Where(o => o.Session.Contains(session ?? ""))
                .Skip(page * pageSize).Take(pageSize);
            return Places;
        }
    }
}