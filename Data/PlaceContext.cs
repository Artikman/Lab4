using Lab_4.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Lab_4.Data
{
    public class PlaceContext
    {
        public static List<Place> GetAll()
        {
            List<Place> all = new List<Place>();
            using (CinemaContext db = new CinemaContext())
            {
                all = db.Places.ToList();
            }

            return all;
        }

        public static List<Place> GetPage(int pageNumber, int sizeOfPage)
        {
            List<Place> all = new List<Place>();
            using (CinemaContext db = new CinemaContext())
            {
                all = db.Places.Include(t => t.Session)
                    .Include(t => t.PlaceNumber).
                    Skip(pageNumber * sizeOfPage).Take(sizeOfPage).ToList();
            }
            return all;
        }

        public static void AddPlace(Place place)
        {
            using (CinemaContext db = new CinemaContext())
            {
                db.Places.Add(place);
                db.SaveChanges();
            }
        }

        public static void UpdataPlace(Place place)
        {
            using (CinemaContext db = new CinemaContext())
            {
                if (place != null)
                {
                    db.Places.Update(place);
                    db.SaveChanges();
                }
            }
        }

        public static void DeletePlace(Place place)
        {
            using (CinemaContext db = new CinemaContext())
            {
                if (place != null)
                {
                    db.Places.Remove(place);
                    db.SaveChanges();
                }
            }
        }

        public static List<Place> FindPlace(string session, int placeNumber)
        {
            List<Place> places = new List<Place>();
            using (CinemaContext db = new CinemaContext())
            {
                if (session != null && session != "")
                {
                    places = db.Places.Where(k => k.Session == session).ToList();
                }
            }
            return places;
        }

        public static Place FindPlaceById(int id)
        {
            Place place = null;
            using (CinemaContext db = new CinemaContext())
            {
                place = db.Places.Where(k => k.PlaceId == id).ToList().FirstOrDefault();
            }
            return place;
        }
    }
}