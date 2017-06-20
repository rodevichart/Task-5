namespace VideoRent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'72724580-d4ea-4019-b9dd-f49ff6ea6f41', N'guest@videorent.com', 0, N'AChvaasK1LPob7my2DQcNu3O4ZEPjS3yLrYW/l4ErLgdWB7jBv899TDZjlKY2V3Mog==', N'68d0d0b0-85f7-483e-b926-60d76a0c4458', NULL, 0, 0, NULL, 1, 0, N'guest@videorent.com')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e701e579-d0ef-4232-a5b4-fc1bd95ece4c', N'admin@videorent.com', 0, N'AAJgSnX314Wpz54m+ML2HhWB2hJgS1kQ3V4GZ3w4CVwiUGUmhMA270qfIeJJ7We4kQ==', N'e0bc36b7-8a30-40af-b97a-b5045eb9ae30', NULL, 0, 0, NULL, 1, 0, N'admin@videorent.com')

INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f4d02841-d8ab-4a7a-ba88-8be0b01cbeda', N'CanManageMiviesCustomers')

INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e701e579-d0ef-4232-a5b4-fc1bd95ece4c', N'f4d02841-d8ab-4a7a-ba88-8be0b01cbeda')

");
        }
        
        public override void Down()
        {
        }
    }
}
