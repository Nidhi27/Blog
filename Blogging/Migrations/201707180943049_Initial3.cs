namespace Blogging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostTags", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostTags", "TagId", "dbo.Tags");
            DropIndex("dbo.PostTags", new[] { "PostId" });
            DropIndex("dbo.PostTags", new[] { "TagId" });
            DropColumn("dbo.Posts", "PostTagId");
            DropColumn("dbo.Tags", "PostTagId");
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
            
            AddColumn("dbo.Tags", "PostTagId", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "PostTagId", c => c.Int(nullable: false));
            CreateIndex("dbo.PostTags", "TagId");
            CreateIndex("dbo.PostTags", "PostId");
            AddForeignKey("dbo.PostTags", "TagId", "dbo.Tags", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostTags", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
    }
}
