using System;

namespace VideoRent.Models
{
    public class Rental
    {
        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}