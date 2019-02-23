namespace SoftwareProjectManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTaskTable3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "AssignedBy", "dbo.Users");
            DropForeignKey("dbo.Tasks", "AssignedTo", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "AssignedBy" });
            DropIndex("dbo.Tasks", new[] { "AssignedTo" });
            RenameColumn(table: "dbo.Tasks", name: "AssignedBy", newName: "AssignedById");
            RenameColumn(table: "dbo.Tasks", name: "AssignedTo", newName: "AssignedToId");
            AlterColumn("dbo.Tasks", "AssignedById", c => c.Int(nullable: false));
            AlterColumn("dbo.Tasks", "AssignedToId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tasks", "AssignedToId");
            CreateIndex("dbo.Tasks", "AssignedById");
            AddForeignKey("dbo.Tasks", "AssignedById", "dbo.Users", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Tasks", "AssignedToId", "dbo.Users", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "AssignedToId", "dbo.Users");
            DropForeignKey("dbo.Tasks", "AssignedById", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "AssignedById" });
            DropIndex("dbo.Tasks", new[] { "AssignedToId" });
            AlterColumn("dbo.Tasks", "AssignedToId", c => c.Int());
            AlterColumn("dbo.Tasks", "AssignedById", c => c.Int());
            RenameColumn(table: "dbo.Tasks", name: "AssignedToId", newName: "AssignedTo");
            RenameColumn(table: "dbo.Tasks", name: "AssignedById", newName: "AssignedBy");
            CreateIndex("dbo.Tasks", "AssignedTo");
            CreateIndex("dbo.Tasks", "AssignedBy");
            AddForeignKey("dbo.Tasks", "AssignedTo", "dbo.Users", "Id");
            AddForeignKey("dbo.Tasks", "AssignedBy", "dbo.Users", "Id");
        }
    }
}
