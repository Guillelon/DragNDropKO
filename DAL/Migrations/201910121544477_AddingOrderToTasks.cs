namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    public partial class AddingOrderToTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskEntries", "Order", c => c.Int(nullable: false));
            var sql = "DECLARE @orderAux int " +
                     " SET @orderAux = 0 " +
                     " UPDATE [TaskEntries] " +
                     " SET @orderAux = [Order] = @orderAux + 1";
            Sql(sql);
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskEntries", "Order");
        }
    }
}
