namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Completed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TaskEntries");
        }
    }
}
