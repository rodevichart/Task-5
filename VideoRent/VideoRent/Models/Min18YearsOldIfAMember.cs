using System;
using System.ComponentModel.DataAnnotations;

namespace VideoRent.Models
{
    public class Min18YearsOldIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer) validationContext.ObjectInstance;
            if (customer.MembershipTypeId == MembershipType.Uknown || customer.MembershipTypeId == MembershipType.PatAsYouGo)
                return ValidationResult.Success;
            if (customer.Birthdate == null)
                return new ValidationResult("Birthdate is required.");
            var age = DateTime.Today.Date.Year - customer.Birthdate.Value.Year;
            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Customer must be 18 year old.");
        }
    }
}