namespace SoftwareProjectManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTable2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "State");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "State", c => c.String(nullable: false));
        }
    }
}
