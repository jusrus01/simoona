namespace Shrooms.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsQueueAllowedPropertyToEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "IsQueueAllowed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "IsQueueAllowed");
        }
    }
}
