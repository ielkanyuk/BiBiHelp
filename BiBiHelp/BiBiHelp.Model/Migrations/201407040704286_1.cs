namespace BiBiHelp.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(),
                        isAnonymous = c.Boolean(nullable: false),
                        Region = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false, storeType: "datetime2"),
                        DateUpdated = c.DateTime(storeType: "datetime2"),
                        Author_Id = c.Guid(),
                        Parent_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id)
                .ForeignKey("dbo.Messages", t => t.Parent_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false, storeType: "datetime2"),
                        DateUpdated = c.DateTime(storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Parent_Id", "dbo.Messages");
            DropForeignKey("dbo.Messages", "Author_Id", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "Parent_Id" });
            DropIndex("dbo.Messages", new[] { "Author_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
        }
    }
}
