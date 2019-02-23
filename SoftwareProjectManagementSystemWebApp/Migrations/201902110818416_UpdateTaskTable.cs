namespace SoftwareProjectManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTaskTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "AssignedTo", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "AssignedTo" });
            AlterColumn("dbo.Tasks", "AssignedTo", c => c.Int());
            CreateIndex("dbo.Tasks", "AssignedTo");
            AddForeignKey("dbo.Tasks", "AssignedTo", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "AssignedTo", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "AssignedTo" });
            AlterColumn("dbo.Tasks", "AssignedTo", c => c.Int(nullable: false));
            CreateIndex("dbo.Tasks", "AssignedTo");
            AddForeignKey("dbo.Tasks", "AssignedTo", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
