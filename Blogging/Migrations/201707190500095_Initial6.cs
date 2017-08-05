namespace Blogging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostTags", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostTags", "TagId", "dbo.Tags");
            DropIndex("dbo.PostTags", new[] { "PostId" });
            DropIndex("dbo.PostTags", new[] { "TagId" });
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
            
            DropTable("dbo.PostTags");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PostTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.TagPosts", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.TagPosts", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagPosts", new[] { "Post_Id" });
            DropIndex("dbo.TagPosts", new[] { "Tag_Id" });
            DropTable("dbo.TagPosts");
            CreateIndex("dbo.PostTags", "TagId");
            CreateIndex("dbo.PostTags", "PostId");
            AddForeignKey("dbo.PostTags", "TagId", "dbo.Tags", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostTags", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
    }
}
