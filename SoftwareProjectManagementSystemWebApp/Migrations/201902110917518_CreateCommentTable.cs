namespace SoftwareProjectManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCommentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                        CommentDescription = c.String(),
                        ActionById = c.Int(nullable: false),
                        ActionTime = c.String(),
                        ActionDone = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectDetails", t => t.ProjectId, cascadeDelete: false)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.ActionById, cascadeDelete: false)
                .Index(t => t.ProjectId)
                .Index(t => t.TaskId)
                .Index(t => t.ActionById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ActionById", "dbo.Users");
            DropForeignKey("dbo.Comments", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Comments", "ProjectId", "dbo.ProjectDetails");
            DropIndex("dbo.Comments", new[] { "ActionById" });
            DropIndex("dbo.Comments", new[] { "TaskId" });
            DropIndex("dbo.Comments", new[] { "ProjectId" });
            DropTable("dbo.Comments");
        }
    }
}
