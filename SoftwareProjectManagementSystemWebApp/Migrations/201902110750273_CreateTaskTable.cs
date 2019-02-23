namespace SoftwareProjectManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTaskTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        DueDate = c.String(nullable: false),
                        Priority = c.String(nullable: false),
                        ActionDone = c.String(),
                        ActionTime = c.String(),
                        AssignedBy = c.Int(),
                        AssignedTo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectDetails", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.AssignedBy)
                .ForeignKey("dbo.Users", t => t.AssignedTo, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.AssignedBy)
                .Index(t => t.AssignedTo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "AssignedTo", "dbo.Users");
            DropForeignKey("dbo.Tasks", "AssignedBy", "dbo.Users");
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.ProjectDetails");
            DropIndex("dbo.Tasks", new[] { "AssignedTo" });
            DropIndex("dbo.Tasks", new[] { "AssignedBy" });
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropTable("dbo.Tasks");
        }
    }
}
