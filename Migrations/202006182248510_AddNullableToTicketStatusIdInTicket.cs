namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableToTicketStatusIdInTicket : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "TicketStatusId", "dbo.TicketStatus");
            DropIndex("dbo.Tickets", new[] { "TicketStatusId" });
            AlterColumn("dbo.Tickets", "TicketStatusId", c => c.Int());
            CreateIndex("dbo.Tickets", "TicketStatusId");
            AddForeignKey("dbo.Tickets", "TicketStatusId", "dbo.TicketStatus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TicketStatusId", "dbo.TicketStatus");
            DropIndex("dbo.Tickets", new[] { "TicketStatusId" });
            AlterColumn("dbo.Tickets", "TicketStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "TicketStatusId");
            AddForeignKey("dbo.Tickets", "TicketStatusId", "dbo.TicketStatus", "Id", cascadeDelete: true);
        }
    }
}
