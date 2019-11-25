namespace ClaimSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friday : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClaimDetails",
                c => new
                    {
                        ClaimDetailId = c.Int(nullable: false),
                        ApprovedBy = c.String(nullable: false),
                        ApprovedAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InternalNotes = c.String(nullable: false),
                        ClaimId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClaimDetailId)
                .ForeignKey("dbo.ReimbursementClaim", t => t.ClaimDetailId)
                .Index(t => t.ClaimDetailId);
            
            CreateTable(
                "dbo.ReimbursementClaim",
                c => new
                    {
                        ClaimId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ReimbursementType = c.String(nullable: false),
                        RequestedValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ApprovedValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(nullable: false),
                        IsProcessed = c.Boolean(nullable: false),
                        Status = c.Boolean(),
                        UploadedFilePath = c.String(maxLength: 255),
                        ClaimOwnerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ClaimId)
                .ForeignKey("dbo.User", t => t.ClaimOwnerId, cascadeDelete: true)
                .Index(t => t.ClaimOwnerId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(nullable: false),
                        PanNumber = c.String(nullable: false),
                        Bank = c.String(nullable: false),
                        BankAccountNumber = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 256),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.ClaimDetails", "ClaimDetailId", "dbo.ReimbursementClaim");
            DropForeignKey("dbo.ReimbursementClaim", "ClaimOwnerId", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropIndex("dbo.Role", "RoleNameIndex");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.ReimbursementClaim", new[] { "ClaimOwnerId" });
            DropIndex("dbo.ClaimDetails", new[] { "ClaimDetailId" });
            DropTable("dbo.Role");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.ReimbursementClaim");
            DropTable("dbo.ClaimDetails");
        }
    }
}
