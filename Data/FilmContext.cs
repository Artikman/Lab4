using Lab_4.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_4.Data
{
    public class FilmContext
    {
        public static List<Film> GetAll()
        {
            List<Film> all = new List<Film>();
            using (CinemaContext db = new CinemaContext())
            {
                all = db.Films.ToList();
            }

            return all;
        }

        public static List<Film> GetPage(int pageNumber, int sizeOfPage)
        {
            List<Film> all = new List<Film>();
            using (CinemaContext db = new CinemaContext())
            {
                all = db.Films.Include(t => t.Name)
                    .Include(t => t.Genre).Include(t => t.FilmCompany).
                    Skip(pageNumber * sizeOfPage).Take(sizeOfPage).ToList();
            }
            return all;
        }

        public static void AddFilm(Film film)
        {
            using (CinemaContext db = new CinemaContext())
            {
                db.Films.Add(film);
                db.SaveChanges();
            }
        }

        public static void UpdataFilms(Film film)
        {
            using (CinemaContext db = new CinemaContext())
            {
                if (film != null)
                {
                    db.Films.Update(film);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteFilm(Film film)
        {
            using (CinemaContext db = new CinemaContext())
            {
                if (film != null)
                {
                    db.Films.Remove(film);
                    db.SaveChanges();
                }
            }
        }

        public static List<Film> FindFilms(int filmId, string name,
            string genre, DateTime duration)
        {
            List<Film> films = new List<Film>();
            using (CinemaContext db = new CinemaContext())
            {
                if (filmId != null)
                {
                    films = db.Films.Where(k => k.FilmId == filmId).ToList();
                }
                if (name != null)
                {
                    films = films.Where(k => k.Name == name).ToList();
                }
                if (genre != null)
                {
                    films = films.Where(k => k.Genre == genre).ToList();
                }
                if (duration != null)
                {
                    if (films.Count != 0)
                    {
                        films = films.Where(k => k.Duration == duration).ToList();
                    }
                    else
                    {
                        films = db.Films.Where(k => k.Duration == duration).ToList();
                    }
                }
            }
            return films;
        }

        public static Film FindFilmById(int id)
        {
            Film film = null;
            using (CinemaContext db = new CinemaContext())
            {
                film = db.Films.Where(k => k.FilmId == id).ToList().FirstOrDefault();
            }
            return film;
        }
    }
}