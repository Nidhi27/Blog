namespace Blogging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "TagId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "TagId");
        }
    }
}
