namespace jobs_offer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditJobModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.jobs", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.jobs", "UserId");
            AddForeignKey("dbo.jobs", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.jobs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.jobs", new[] { "UserId" });
            DropColumn("dbo.jobs", "UserId");
        }
    }
}
