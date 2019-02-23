namespace SoftwareProjectManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTableWithStateField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "State", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "State");
        }
    }
}
