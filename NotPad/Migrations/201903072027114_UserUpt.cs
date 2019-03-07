namespace NotPad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserUpt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        DisplayName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Notes", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Notes", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.Notes", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Notes", "UserID");
          

            Sql(@"
                INSERT INTO dbo.Users ([UserName],[Password],[DisplayName]) VALUES ('bilal','211','Bilal Y.')
            ");

            Sql(@"
                UPDATE dbo.Notes SET [CreateDate] = GETDATE(), UserID = 1
            ");

            AddForeignKey("dbo.Notes", "UserID", "dbo.Users", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "UserID", "dbo.Users");
            DropIndex("dbo.Notes", new[] { "UserID" });
            DropColumn("dbo.Notes", "UserID");
            DropColumn("dbo.Notes", "UpdateTime");
            DropColumn("dbo.Notes", "CreateDate");
            DropTable("dbo.Users");
        }
    }
}
