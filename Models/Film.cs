using System;

namespace Lab_4.Models
{
    public class Film
    {
        public int FilmId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public DateTime Duration { get; set; }
        public string FilmCompany { get; set; }
        public string ProducingCountry { get; set; }
        public string ListOfMainActros { get; set; }
        public int AgeRestrictions { get; set; }
        public string Description { get; set; }
        public int GenreId { get; set; }
        public int SessionId { get; set; }
        public virtual Session Sessions { get; set; }
        public virtual Genre Genres { get; set; }

        public Film() { }

        public Film(int FilmId, string Name, string Genre, DateTime Duration, string FilmCompany, string ProducingCountry, string ListOfMainActros, int AgeRestrictions, string Description, int GenreId, int SessionId)
        {
            this.FilmId = FilmId;
            this.Name = Name;
            this.Genre = Genre;
            this.Duration = Duration;
            this.FilmCompany = FilmCompany;
            this.ProducingCountry = ProducingCountry;
            this.ListOfMainActros = ListOfMainActros;
            this.AgeRestrictions = AgeRestrictions;
            this.Description = Description;
            this.GenreId = GenreId;
            this.SessionId = SessionId;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Film;

            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }

            return this.FilmId == item.FilmId;
        }

        public override int GetHashCode()
        {
            return this.FilmId.GetHashCode();
        }
    }
}