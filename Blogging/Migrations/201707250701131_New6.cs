namespace Blogging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "TagId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "TagId");
            AddForeignKey("dbo.Posts", "TagId", "dbo.Tags", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "TagId", "dbo.Tags");
            DropIndex("dbo.Posts", new[] { "TagId" });
            DropColumn("dbo.Posts", "TagId");
        }
    }
}
