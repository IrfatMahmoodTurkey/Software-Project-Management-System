namespace SoftwareProjectManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAssignUserTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AssignUsers", "ActionBy", c => c.Int(nullable: false));
            CreateIndex("dbo.AssignUsers", "ActionBy");
            AddForeignKey("dbo.AssignUsers", "ActionBy", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignUsers", "ActionBy", "dbo.Users");
            DropIndex("dbo.AssignUsers", new[] { "ActionBy" });
            AlterColumn("dbo.AssignUsers", "ActionBy", c => c.String());
        }
    }
}
