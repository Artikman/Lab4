using Lab_4.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Lab_4.Data
{
    public class GenreContext
    {
        public static List<Genre> GetAll()
        {
            List<Genre> all = new List<Genre>();
            using (CinemaContext db = new CinemaContext())
            {
                all = db.Genres.ToList();
            }
            return all;
        }

        public static List<Genre> GetPage(int pageNumber, int sizeOfPage)
        {
            List<Genre> all = new List<Genre>();
            using (CinemaContext db = new CinemaContext())
            {
                all = db.Genres.Include(t => t.Name)
                    .Include(t => t.Description).
                    Skip(pageNumber * sizeOfPage).Take(sizeOfPage).ToList();
            }
            return all;
        }

        public static void AddGenre(Genre genre)
        {
            using (CinemaContext db = new CinemaContext())
            {
                db.Genres.Add(genre);
                db.SaveChanges();
            }
        }

        public static void UpdateGenre(Genre genre)
        {
            using (CinemaContext db = new CinemaContext())
            {
                if (genre != null)
                {
                    db.Genres.Update(genre);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteGenre(Genre genre)
        {
            using (CinemaContext db = new CinemaContext())
            {
                if (genre != null)
                {
                    db.Genres.Remove(genre);
                    db.SaveChanges();
                }
            }
        }

        public static List<Genre> FindGenres(string name, string description)
        {
            List<Genre> genres = new List<Genre>();
            using (CinemaContext db = new CinemaContext())
            {
                if (name != null && name != "")
                {
                    genres = db.Genres.Where(k => k.Name == name).ToList();
                }
                if (description != null && description != "")
                {
                    if (genres.Count != 0)
                    {
                        genres = genres.Where(k => k.Description == description).ToList();
                    }
                    else
                    {
                        genres = db.Genres.Where(k => k.Description == description).ToList();
                    }
                }
            }
            return genres;
        }

        public static Genre FindGenreById(int id)
        {
            Genre genre = null;
            using (CinemaContext db = new CinemaContext())
            {
                genre = db.Genres.Where(k => k.GenreId == id).ToList().FirstOrDefault();
            }
            return genre;
        }
    }
}