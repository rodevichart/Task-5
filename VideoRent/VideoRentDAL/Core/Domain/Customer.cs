using System;
using System.ComponentModel.DataAnnotations;

namespace VideoRentDAL.Core.Domain
{
    public class Customer
    {
        public int Id { get; set; }      
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}