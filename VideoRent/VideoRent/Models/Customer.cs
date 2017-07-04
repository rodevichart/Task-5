using System;
using System.ComponentModel.DataAnnotations;
using VideoRent.Resourses;

namespace VideoRent.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(NewCustomer))]
        [Required(ErrorMessageResourceType = typeof (NewCustomer), ErrorMessageResourceName = "CustomerNameRequiredMessage")]
        [StringLength(255)]       
        public string Name { get; set; }
               
        public MembershipType MembershipType { get; set; }

        [Display(Name = "MembershipType", ResourceType = typeof(NewCustomer))]
        public byte MembershipTypeId { get; set; }

        [Display(Name = "SubscribedToNewsletter", ResourceType = typeof(NewCustomer))]
        public bool IsSubscribedToNewsletter { get; set; }

        [Min18YearsOldIfAMember]
        [Display(Name = "Dayofbirth", ResourceType = typeof(NewCustomer))]
        public DateTime? Birthdate { get; set; }
    }
}