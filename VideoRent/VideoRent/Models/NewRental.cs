using System.Collections.Generic;

namespace VideoRent.Models
{
    public class NewRental
    {
        public NewRental()
        {
            MovieIds = new List<int>();
        }    
        public List<int> MovieIds { get; set; }
        public int CustomerId { get; set; }
    }
}