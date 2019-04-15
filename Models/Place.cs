using System.Collections.Generic;

namespace Lab_4.Models
{
    public class Place
    {
        public int PlaceId { get; set; }
        public string Session { get; set; }
        public int PlaceNumber { get; set; }
        public bool Employment { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }

        public Place()
        {
            Sessions = new List<Session>();
        }

        public Place(int PlaceId, string Session, int PlaceNumber, bool Employment)
        {
            this.PlaceId = PlaceId;
            this.Session = Session;
            this.PlaceNumber = PlaceNumber;
            this.Employment = Employment;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Place;

            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }

            return this.PlaceId == item.PlaceId;
        }

        public override int GetHashCode()
        {
            return this.PlaceId.GetHashCode();
        }
    }
}