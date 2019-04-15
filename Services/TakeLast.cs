using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Lab_4.ViewModels;
using Lab_4.Models;
using Lab_4.Data;

namespace Lab_4.Services
{
    public class TakeLast
    {
        public static HomeViewModel GetHomeViewModel()
        {
            HomeViewModel homeViewModel = null;
            using (CinemaContext _context = new CinemaContext())
            {
                List<Genre> genres = _context.Genres.OrderByDescending(p => p.GenreId).Take(5).ToList();
                List<Film> films = _context.Films.OrderByDescending(p => p.FilmId).Take(5).ToList();
                List<Session> sessions = _context.Sessions.OrderByDescending(p => p.SessionId).Take(5).ToList();
                List<Place> places = _context.Places.OrderByDescending(p => p.PlaceId).Take(5).ToList();
                homeViewModel = new HomeViewModel
                {
                    Genres = genres,
                    Films = films,
                    Sessions = sessions,
                    Places = places
                };
            }

            return homeViewModel;
        }
    }
}