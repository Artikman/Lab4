using Lab_4.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Lab_4.Data
{
    public class SessionContext
    {
        public static List<Session> GetAll()
        {
            List<Session> all = new List<Session>();
            using (CinemaContext db = new CinemaContext())
            {
                all = db.Sessions.ToList();
            }

            return all;
        }

        public static List<Session> GetPage(int pageNumber, int sizeOfPage)
        {
            List<Session> all = new List<Session>();
            using (CinemaContext db = new CinemaContext())
            {
                all = db.Sessions.Include(t => t.Date)
                    .Include(t => t.TimeStarted).Include(t => t.EndTime).
                    Skip(pageNumber * sizeOfPage).Take(sizeOfPage).ToList();
            }
            return all;
        }

        public static void AddSession(Session session)
        {
            using (CinemaContext db = new CinemaContext())
            {
                db.Sessions.Add(session);
                db.SaveChanges();
            }
        }

        public static void UpdataSession(Session session)
        {
            using (CinemaContext db = new CinemaContext())
            {
                if (session != null)
                {
                    db.Sessions.Update(session);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteSession(Session session)
        {
            using (CinemaContext db = new CinemaContext())
            {
                if (session != null)
                {
                    db.Sessions.Remove(session);
                    db.SaveChanges();
                }
            }
        }

        public static Session FindSessionById(int id)
        {
            Session session = null;
            using (CinemaContext db = new CinemaContext())
            {
                session = db.Sessions.Where(k => k.SessionId == id).ToList().FirstOrDefault();
            }
            return session;
        }
    }
}