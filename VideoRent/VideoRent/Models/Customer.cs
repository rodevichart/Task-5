using System;
using System.ComponentModel.DataAnnotations;

namespace VideoRent.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Customer name.")]
        [StringLength(255)]
        [Display(Name = "Name", ResourceType = typeof(Resourses.NewCustomer))]
        public string Name { get; set; }
               
        public MembershipType MembershipType { get; set; }

        [Display(Name = "MembershipType", ResourceType = typeof(Resourses.NewCustomer))]
        public byte MembershipTypeId { get; set; }

        [Display(Name = "SubscribedToNewsletter", ResourceType = typeof(Resourses.NewCustomer))]
        public bool IsSubscribedToNewsletter { get; set; }

        [Min18YearsOldIfAMember]
        [Display(Name = "Dayofbirth", ResourceType = typeof(Resourses.NewCustomer))]
        public DateTime? Birthdate { get; set; }
    }
}