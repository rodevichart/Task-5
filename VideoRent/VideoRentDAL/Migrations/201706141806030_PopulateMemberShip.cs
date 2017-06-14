namespace VideoRentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMemberShip : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MembershipTypes", name: "SingUpFree", newName: "SingUpFee");
            Sql("Insert into MembershipTypes (Id, Name, SingUpFee, DurationInMonths, DiscountRate) Values (1, 'Pay as You Go', 0, 0, 0)");
            Sql("Insert into MembershipTypes (Id, Name, SingUpFee, DurationInMonths, DiscountRate) Values (2, 'Monthly', 30, 1, 10)");
            Sql("Insert into MembershipTypes (Id, Name, SingUpFee, DurationInMonths, DiscountRate) Values (3, 'Quarterly', 90, 3, 15)");
            Sql("Insert into MembershipTypes (Id, Name, SingUpFee, DurationInMonths, DiscountRate) Values (4, 'Annual', 300, 12, 20)");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.MembershipTypes", name: "SingUpFee", newName: "SingUpFree");
        }
    }
}
