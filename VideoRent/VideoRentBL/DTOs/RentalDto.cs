using System;

namespace VideoRentBL.DTOs
{
    public class RentalDto
    {
        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public int MovieId { get; set; }
        public MovieDto Movie { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }        
    }
}