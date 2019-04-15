using System;
using System.Collections.Generic;

namespace Lab_4.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeStarted { get; set; }
        public DateTime EndTime { get; set; }
        public float TicketPrice { get; set; }
        public string EmployessInvolvedInTheSession { get; set; }
        public int PlaceId { get; set; }
        public Place Places { get; set; }
        public virtual ICollection<Film> Films { get; set; }

        public Session() { }

        public Session(int SessionId, DateTime Date, DateTime TimeStarted, DateTime EndTime, float TicketPrice, string EmployessInvolvedInTheSession, int PlaceId)
        {
            this.SessionId = SessionId;
            this.Date = Date;
            this.TimeStarted = TimeStarted;
            this.EndTime = EndTime;
            this.TicketPrice = TicketPrice;
            this.EmployessInvolvedInTheSession = EmployessInvolvedInTheSession;
            this.PlaceId = PlaceId;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Session;

            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }

            return this.SessionId == item.SessionId;
        }

        public override int GetHashCode()
        {
            return this.SessionId.GetHashCode();
        }
    }
}