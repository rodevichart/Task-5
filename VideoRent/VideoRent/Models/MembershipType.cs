﻿namespace VideoRent.Models
{
    public class MembershipType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short SingUpFree { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        public static readonly byte Uknown = 0;
        public static readonly byte PatAsYouGo = 1;
    }
}