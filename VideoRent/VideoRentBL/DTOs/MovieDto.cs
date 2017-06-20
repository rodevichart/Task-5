using System;

namespace VideoRentBL.DTOs
{
    public class MovieDto : IComparable<MovieDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GenreDto Genre { get; set; }
        public byte GenreId { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ReleaseDate { get; set; }
        public byte NumberInStock { get; set; }
        public int CompareTo(MovieDto movie)
        {
            if (this.Id == movie.Id)
            {
                return this.Name.CompareTo(movie.Name);
            }
            return movie.Id.CompareTo(this.Id);
        }
    }
}