namespace SoftwareProjectManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCommentTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "CommentDescription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "CommentDescription", c => c.String());
        }
    }
}
