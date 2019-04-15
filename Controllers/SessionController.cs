using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Lab_4.Data;
using Lab_4.Models;
using Lab_4.ViewModels;
using Lab_4.Filters;
using Newtonsoft.Json;

namespace Lab_4.Controllers
{
    [CatchExceptionFilter]
    public class SessionController : Controller
    {
        private int pageSize = 3;
        private CinemaContext db;
        private Session _session = new Session
        {
            EmployessInvolvedInTheSession = "",
        };

        public SessionController(CinemaContext sessionContext)
        {
            db = sessionContext;
        }

        [HttpGet]
        public IActionResult Index(SortState sortOrder = SortState.No, int index = 0)
        {
            Session sessionSession = (Session)HttpContext.Session.GetObject("Session");
            string sessionSortState = HttpContext.Session.GetString("SortState");
            int? page = HttpContext.Session.GetInt32("Page");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("Page", 0);
            }
            else
            {
                if (!(page < 1 && index < 0))
                    page += index;
                HttpContext.Session.SetInt32("Page", (int)page);
            }

            if(sessionSession != null)
            {
                _session = sessionSession;
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            ViewData["EmployessInvolvedInTheSessionSort"] = sortOrder == SortState.EmployessInvolvedInTheSessionDesc ? SortState.EmployessInvolvedInTheSessionAsc : SortState.EmployessInvolvedInTheSessionDesc;

            HttpContext.Session.SetString("SortState", sortOrder.ToString());
            IQueryable<Session> sessions = Sort(db.Sessions, sortOrder,
                _session.EmployessInvolvedInTheSession, (int)page);
            SessionsViewModel sessionsView = new SessionsViewModel
            {
                SessionViewModel = _session,
                PageViewModel = sessions,
                PageNumber = (int)page
            };

            return View(sessionsView);
        }

        [HttpPost]
        public IActionResult Index(Session session)
        {
            var sessionSortState = HttpContext.Session.GetString("SortState");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            int? page = HttpContext.Session.GetInt32("Page");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("Page", 0);
            }

            IQueryable<Session> sessions = Sort(db.Sessions, sortOrder,
                session.EmployessInvolvedInTheSession, (int)page);
            HttpContext.Session.SetObject("Session", session);

            SessionsViewModel sessionsView = new SessionsViewModel
            {
                SessionViewModel = session,
                PageViewModel = sessions,
                PageNumber = (int)page
            };

            return View(sessionsView);
        }

        private IQueryable<Session> Sort(IQueryable<Session> sessions,
            SortState sortOrder, string employessInvolvedInTheSession, int page)
        {
            switch (sortOrder)
            {
                case SortState.EmployessInvolvedInTheSessionAsc:
                    sessions = sessions.OrderBy(s => s.EmployessInvolvedInTheSession);
                    break;
                case SortState.EmployessInvolvedInTheSessionDesc:
                    sessions = sessions.OrderByDescending(s => s.EmployessInvolvedInTheSession);
                    break;
            }
            sessions = sessions.Where(o => o.EmployessInvolvedInTheSession.Contains(employessInvolvedInTheSession ?? "")).Skip(page * pageSize).Take(pageSize);
            return sessions;
        }

        private void SetSession2(string session2)
        {
            _session.EmployessInvolvedInTheSession = session2.Split(':')[0];
        }
    }
}