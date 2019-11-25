namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Name = c.String(nullable: false, maxLength: 256),
                        CreationDate = c.DateTime(),
                        CreatorId = c.String(maxLength: 128),
                        LastUpdateDate = c.DateTime(),
                        LastUpdateUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId)
                .ForeignKey("dbo.AspNetUsers", t => t.LastUpdateUserId)
                .Index(t => t.CreatorId)
                .Index(t => t.LastUpdateUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        ControllerName = c.String(maxLength: 256),
                        ParentMenuId = c.Int(),
                        DefaultAction = c.String(),
                        Disabled = c.Boolean(nullable: false),
                        Hidden = c.Boolean(nullable: false),
                        Icon = c.String(),
                        OrderNumber = c.Int(),
                        EditableRights = c.Boolean(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        CreationDate = c.DateTime(),
                        CreatorId = c.String(maxLength: 128),
                        LastUpdateDate = c.DateTime(),
                        LastUpdateUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId)
                .ForeignKey("dbo.AspNetUsers", t => t.LastUpdateUserId)
                .ForeignKey("dbo.MenuItems", t => t.ParentMenuId)
                .Index(t => t.ControllerName, unique: true)
                .Index(t => t.ParentMenuId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.CreatorId)
                .Index(t => t.LastUpdateUserId);
            
            CreateTable(
                "dbo.Rights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.String(maxLength: 128),
                        MenuItemId = c.Int(),
                        View = c.Boolean(nullable: false),
                        Create = c.Boolean(nullable: false),
                        Edit = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        Export = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        CreatorId = c.String(maxLength: 128),
                        LastUpdateDate = c.DateTime(),
                        LastUpdateUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId)
                .ForeignKey("dbo.AspNetUsers", t => t.LastUpdateUserId)
                .ForeignKey("dbo.MenuItems", t => t.MenuItemId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.MenuItemId)
                .Index(t => t.CreatorId)
                .Index(t => t.LastUpdateUserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex")
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Rights", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Rights", "MenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.Rights", "LastUpdateUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rights", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MenuItems", "ParentMenuId", "dbo.MenuItems");
            DropForeignKey("dbo.MenuItems", "LastUpdateUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MenuItems", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Documents", "LastUpdateUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Documents", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", new[] { "Name" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Rights", new[] { "LastUpdateUserId" });
            DropIndex("dbo.Rights", new[] { "CreatorId" });
            DropIndex("dbo.Rights", new[] { "MenuItemId" });
            DropIndex("dbo.Rights", new[] { "RoleId" });
            DropIndex("dbo.MenuItems", new[] { "LastUpdateUserId" });
            DropIndex("dbo.MenuItems", new[] { "CreatorId" });
            DropIndex("dbo.MenuItems", new[] { "Name" });
            DropIndex("dbo.MenuItems", new[] { "ParentMenuId" });
            DropIndex("dbo.MenuItems", new[] { "ControllerName" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Documents", new[] { "LastUpdateUserId" });
            DropIndex("dbo.Documents", new[] { "CreatorId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Rights");
            DropTable("dbo.MenuItems");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Documents");
        }
    }
}
