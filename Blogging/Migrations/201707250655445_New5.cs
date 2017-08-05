namespace Blogging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "TagId", "dbo.Tags");
            DropIndex("dbo.Posts", new[] { "TagId" });
            DropColumn("dbo.Posts", "TagId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "TagId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "TagId");
            AddForeignKey("dbo.Posts", "TagId", "dbo.Tags", "Id", cascadeDelete: true);
        }
    }
}
