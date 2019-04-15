using System.Linq;
using Lab_4.Models;

namespace Lab_4.ViewModels
{
    public class SessionsViewModel
    {
        public Session SessionViewModel { get; set; }
        public IQueryable<Session> PageViewModel { get; set; }
        public int PageNumber { get; set; }
    }
}