namespace Blogging.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "TagId", "dbo.Tags");
            DropIndex("dbo.Posts", new[] { "TagId" });
            RenameColumn(table: "dbo.Posts", name: "TagId", newName: "Tag_Id");
            AlterColumn("dbo.Posts", "Tag_Id", c => c.Int());
            CreateIndex("dbo.Posts", "Tag_Id");
            AddForeignKey("dbo.Posts", "Tag_Id", "dbo.Tags", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.Posts", new[] { "Tag_Id" });
            AlterColumn("dbo.Posts", "Tag_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Posts", name: "Tag_Id", newName: "TagId");
            CreateIndex("dbo.Posts", "TagId");
            AddForeignKey("dbo.Posts", "TagId", "dbo.Tags", "Id", cascadeDelete: true);
        }
    }
}
