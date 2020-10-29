namespace MessManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberCosttableMonthIdYearIdAttributedadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberCosts", "MonthId", c => c.Int(nullable: false));
            AddColumn("dbo.MemberCosts", "YearId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberCosts", "YearId");
            DropColumn("dbo.MemberCosts", "MonthId");
        }
    }
}
