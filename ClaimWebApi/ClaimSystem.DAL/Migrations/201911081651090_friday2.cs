namespace ClaimSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friday2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ClaimDetails", "ClaimId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClaimDetails", "ClaimId", c => c.Int(nullable: false));
        }
    }
}
