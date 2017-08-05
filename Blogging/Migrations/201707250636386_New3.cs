namespace Blogging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagPosts", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.TagPosts", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.PostTagMappings", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostTagMappings", "TagId", "dbo.Tags");
            DropIndex("dbo.PostTagMappings", new[] { "TagId" });
            DropIndex("dbo.PostTagMappings", new[] { "PostId" });
            DropIndex("dbo.TagPosts", new[] { "Tag_Id" });
            DropIndex("dbo.TagPosts", new[] { "Post_Id" });
            DropIndex("dbo.Posts", "TagId");
            DropForeignKey("dbo.Posts", "TagId", "dbo.Tags");
            DropTable("dbo.PostTagMappings");
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
            
            CreateTable(
                "dbo.PostTagMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Posts", "TagId", "dbo.Tags");
            DropIndex("dbo.Posts", new[] { "TagId" });
            CreateIndex("dbo.TagPosts", "Post_Id");
            CreateIndex("dbo.TagPosts", "Tag_Id");
            CreateIndex("dbo.PostTagMappings", "PostId");
            CreateIndex("dbo.PostTagMappings", "TagId");
            AddForeignKey("dbo.PostTagMappings", "TagId", "dbo.Tags", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostTagMappings", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TagPosts", "Post_Id", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TagPosts", "Tag_Id", "dbo.Tags", "Id", cascadeDelete: true);
        }
    }
}
