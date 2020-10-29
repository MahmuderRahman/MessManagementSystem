namespace MessManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentTablemodified : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Payments", "MemberId");
            AddForeignKey("dbo.Payments", "MemberId", "dbo.MemberEntries", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "MemberId", "dbo.MemberEntries");
            DropIndex("dbo.Payments", new[] { "MemberId" });
        }
    }
}
