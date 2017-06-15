using System.Collections.Generic;

namespace VideoRentBL.DTOs
{
    public class MembershipTypeDto
    {
        public string Name { get; set; }
        public short SingUpFree { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }
        public ICollection<CustomerDto> Customers { get; set; }
    }
}