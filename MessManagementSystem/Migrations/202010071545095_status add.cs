namespace MessManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statusadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberEntries", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberEntries", "Status");
        }
    }
}
