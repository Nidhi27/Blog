namespace Blogging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostTagMappings", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostTagMappings", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.PostTagMappings", new[] { "TagId" });
            DropIndex("dbo.PostTagMappings", new[] { "PostId" });
            AlterColumn("dbo.Posts", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "UserId");
            AddForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers", "Id");
            DropTable("dbo.PostTagMappings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PostTagMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "UserId" });
            AlterColumn("dbo.Posts", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.PostTagMappings", "PostId");
            CreateIndex("dbo.PostTagMappings", "TagId");
            CreateIndex("dbo.Posts", "UserId");
            AddForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostTagMappings", "TagId", "dbo.Tags", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostTagMappings", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
    }
}
