namespace Blogging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagPosts", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.TagPosts", "Post_Id", "dbo.Posts");
            DropIndex("dbo.TagPosts", new[] { "Tag_Id" });
            DropIndex("dbo.TagPosts", new[] { "Post_Id" });
            CreateTable(
                "dbo.PostTagMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.TagId)
                .Index(t => t.PostId);
            
            AddColumn("dbo.Posts", "Tag_Id", c => c.Int());
            CreateIndex("dbo.Posts", "Tag_Id");
            AddForeignKey("dbo.Posts", "Tag_Id", "dbo.Tags", "Id");
            DropTable("dbo.TagPosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Post_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Post_Id });
            
            DropForeignKey("dbo.PostTagMappings", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Posts", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.PostTagMappings", "PostId", "dbo.Posts");
            DropIndex("dbo.PostTagMappings", new[] { "PostId" });
            DropIndex("dbo.PostTagMappings", new[] { "TagId" });
            DropIndex("dbo.Posts", new[] { "Tag_Id" });
            DropColumn("dbo.Posts", "Tag_Id");
            DropTable("dbo.PostTagMappings");
            CreateIndex("dbo.TagPosts", "Post_Id");
            CreateIndex("dbo.TagPosts", "Tag_Id");
            AddForeignKey("dbo.TagPosts", "Post_Id", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TagPosts", "Tag_Id", "dbo.Tags", "Id", cascadeDelete: true);
        }
    }
}
