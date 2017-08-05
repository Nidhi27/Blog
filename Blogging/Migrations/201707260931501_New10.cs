namespace Blogging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostTag",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.TagId })
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostTag", "TagId", "dbo.Tags");
            DropForeignKey("dbo.PostTag", "PostId", "dbo.Posts");
            DropIndex("dbo.PostTag", new[] { "TagId" });
            DropIndex("dbo.PostTag", new[] { "PostId" });
            DropTable("dbo.PostTag");
        }
    }
}
