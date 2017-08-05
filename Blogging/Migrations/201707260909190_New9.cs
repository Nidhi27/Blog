namespace Blogging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagPosts", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.TagPosts", "Post_Id", "dbo.Posts");
            DropIndex("dbo.TagPosts", new[] { "Tag_Id" });
            DropIndex("dbo.TagPosts", new[] { "Post_Id" });
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
            
            CreateIndex("dbo.TagPosts", "Post_Id");
            CreateIndex("dbo.TagPosts", "Tag_Id");
            AddForeignKey("dbo.TagPosts", "Post_Id", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TagPosts", "Tag_Id", "dbo.Tags", "Id", cascadeDelete: true);
        }
    }
}
