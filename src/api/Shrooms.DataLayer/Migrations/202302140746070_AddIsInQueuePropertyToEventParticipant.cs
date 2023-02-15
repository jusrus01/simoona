namespace Shrooms.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsInQueuePropertyToEventParticipant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventParticipants", "IsInQueue", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventParticipants", "IsInQueue");
        }
    }
}
