using System;

namespace VideoRentBL.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public MembershipTypeDto MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}