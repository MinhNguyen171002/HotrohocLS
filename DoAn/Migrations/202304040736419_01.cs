namespace DoAn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GuestUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        PassWord = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Image1",
                c => new
                    {
                        IDImage = c.Int(nullable: false, identity: true),
                        UrlImage = c.String(),
                        IdNV = c.Int(),
                        IdNoiDung = c.Int(),
                    })
                .PrimaryKey(t => t.IDImage)
                .ForeignKey("dbo.NhanVatLS", t => t.IdNV)
                .ForeignKey("dbo.SuKiens", t => t.IdNoiDung)
                .Index(t => t.IdNV)
                .Index(t => t.IdNoiDung);
            
            CreateTable(
                "dbo.NhanVatLS",
                c => new
                    {
                        IdNV = c.Int(nullable: false, identity: true),
                        TenNhanVat = c.String(nullable: false, maxLength: 250),
                        NgaySinh = c.String(nullable: false),
                        NgayMat = c.String(nullable: false),
                        NoiDungTomTatNVUrl = c.String(),
                        IdThoiKy = c.Int(),
                    })
                .PrimaryKey(t => t.IdNV)
                .ForeignKey("dbo.ThoiKies", t => t.IdThoiKy)
                .Index(t => t.IdThoiKy);
            
            CreateTable(
                "dbo.SuKiens",
                c => new
                    {
                        IdNoiDung = c.Int(nullable: false, identity: true),
                        TenNoiDung = c.String(),
                        NoiDungSK = c.String(),
                        TomTatSK = c.String(),
                        IdThoiKy = c.Int(),
                        IdNv = c.String(),
                    })
                .PrimaryKey(t => t.IdNoiDung)
                .ForeignKey("dbo.ThoiKies", t => t.IdThoiKy)
                .Index(t => t.IdThoiKy);
            
            CreateTable(
                "dbo.ThoiKies",
                c => new
                    {
                        IdThoiKy = c.Int(nullable: false, identity: true),
                        TenThoiKy = c.String(nullable: false, maxLength: 250),
                        NamBatDau = c.String(nullable: false),
                        NamKetThuc = c.String(),
                        TomTatTK = c.String(),
                    })
                .PrimaryKey(t => t.IdThoiKy);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SuKienNhanVatLS",
                c => new
                    {
                        SuKien_IdNoiDung = c.Int(nullable: false),
                        NhanVatLS_IdNV = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SuKien_IdNoiDung, t.NhanVatLS_IdNV })
                .ForeignKey("dbo.SuKiens", t => t.SuKien_IdNoiDung, cascadeDelete: true)
                .ForeignKey("dbo.NhanVatLS", t => t.NhanVatLS_IdNV, cascadeDelete: true)
                .Index(t => t.SuKien_IdNoiDung)
                .Index(t => t.NhanVatLS_IdNV);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SuKiens", "IdThoiKy", "dbo.ThoiKies");
            DropForeignKey("dbo.NhanVatLS", "IdThoiKy", "dbo.ThoiKies");
            DropForeignKey("dbo.SuKienNhanVatLS", "NhanVatLS_IdNV", "dbo.NhanVatLS");
            DropForeignKey("dbo.SuKienNhanVatLS", "SuKien_IdNoiDung", "dbo.SuKiens");
            DropForeignKey("dbo.Image1", "IdNoiDung", "dbo.SuKiens");
            DropForeignKey("dbo.Image1", "IdNV", "dbo.NhanVatLS");
            DropIndex("dbo.SuKienNhanVatLS", new[] { "NhanVatLS_IdNV" });
            DropIndex("dbo.SuKienNhanVatLS", new[] { "SuKien_IdNoiDung" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SuKiens", new[] { "IdThoiKy" });
            DropIndex("dbo.NhanVatLS", new[] { "IdThoiKy" });
            DropIndex("dbo.Image1", new[] { "IdNoiDung" });
            DropIndex("dbo.Image1", new[] { "IdNV" });
            DropTable("dbo.SuKienNhanVatLS");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ThoiKies");
            DropTable("dbo.SuKiens");
            DropTable("dbo.NhanVatLS");
            DropTable("dbo.Image1");
            DropTable("dbo.GuestUsers");
        }
    }
}
