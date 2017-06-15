using System;
using System.ComponentModel.DataAnnotations;

namespace VideoRentDAL.Core.Domain
{
    public class Rental
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }        
    }
}