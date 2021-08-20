namespace jobs_offer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Applyjobs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applyforjobs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        message = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        jobid = c.Int(nullable: false),
                        userid = c.String(),
                        users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.jobs", t => t.jobid, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.users_Id)
                .Index(t => t.jobid)
                .Index(t => t.users_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applyforjobs", "users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applyforjobs", "jobid", "dbo.jobs");
            DropIndex("dbo.Applyforjobs", new[] { "users_Id" });
            DropIndex("dbo.Applyforjobs", new[] { "jobid" });
            DropTable("dbo.Applyforjobs");
        }
    }
}
