namespace SoftwareProjectManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAllTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ActionBy = c.String(),
                        ActionDone = c.String(),
                        ActionTime = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectDetails", t => t.ProjectId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectCode = c.String(),
                        ProjectName = c.String(nullable: false),
                        CodeName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        StartDate = c.String(nullable: false),
                        EndDate = c.String(nullable: false),
                        Status = c.String(nullable: false),
                        ActionBy = c.Int(nullable: false),
                        ActionDone = c.String(),
                        ActionTime = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ActionBy, cascadeDelete: false)
                .Index(t => t.ActionBy);
            
            CreateTable(
                "dbo.FileUrls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        FileName = c.String(),
                        FileLocation = c.String(),
                        ActionBy = c.Int(nullable: false),
                        ActionDone = c.String(),
                        ActionTime = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectDetails", t => t.ProjectId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.ActionBy, cascadeDelete: false)
                .Index(t => t.ProjectId)
                .Index(t => t.ActionBy);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Status = c.String(nullable: false, maxLength: 10),
                        DesignationId = c.Int(nullable: false),
                        ActionBy = c.String(),
                        ActionDone = c.String(),
                        ActionTime = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: false)
                .Index(t => t.DesignationId);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.AssignUsers", "ProjectId", "dbo.ProjectDetails");
            DropForeignKey("dbo.ProjectDetails", "ActionBy", "dbo.Users");
            DropForeignKey("dbo.FileUrls", "ActionBy", "dbo.Users");
            DropForeignKey("dbo.Users", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.FileUrls", "ProjectId", "dbo.ProjectDetails");
            DropIndex("dbo.Users", new[] { "DesignationId" });
            DropIndex("dbo.FileUrls", new[] { "ActionBy" });
            DropIndex("dbo.FileUrls", new[] { "ProjectId" });
            DropIndex("dbo.ProjectDetails", new[] { "ActionBy" });
            DropIndex("dbo.AssignUsers", new[] { "UserId" });
            DropIndex("dbo.AssignUsers", new[] { "ProjectId" });
            DropTable("dbo.Designations");
            DropTable("dbo.Users");
            DropTable("dbo.FileUrls");
            DropTable("dbo.ProjectDetails");
            DropTable("dbo.AssignUsers");
        }
    }
}
