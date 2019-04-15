using System.Collections.Generic;

namespace Lab_4.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Film> Films { get; set; }
        public Genre()
        {
            Films = new List<Film>();
        }

        public Genre(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Genre;

            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }

            return this.GenreId == item.GenreId;
        }

        public override int GetHashCode()
        {
            return this.GenreId.GetHashCode();
        }
    }
}