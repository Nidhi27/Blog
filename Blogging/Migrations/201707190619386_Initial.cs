namespace Blogging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.Posts", new[] { "Tag_Id" });
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Post_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Post_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Post_Id);
            
            DropColumn("dbo.Posts", "Tag_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Tag_Id", c => c.Int());
            DropForeignKey("dbo.TagPosts", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.TagPosts", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagPosts", new[] { "Post_Id" });
            DropIndex("dbo.TagPosts", new[] { "Tag_Id" });
            DropTable("dbo.TagPosts");
            CreateIndex("dbo.Posts", "Tag_Id");
            AddForeignKey("dbo.Posts", "Tag_Id", "dbo.Tags", "Id");
        }
    }
}
