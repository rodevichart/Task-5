using System.Collections.Generic;

namespace VideoRentDAL.Core.Domain
{
    public class MembershipType
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public short SingUpFree { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}