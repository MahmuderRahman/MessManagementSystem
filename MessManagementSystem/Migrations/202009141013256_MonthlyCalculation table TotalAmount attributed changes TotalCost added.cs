namespace MessManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MonthlyCalculationtableTotalAmountattributedchangesTotalCostadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MonthlyCalculations", "TotalCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.MonthlyCalculations", "TotalAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MonthlyCalculations", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.MonthlyCalculations", "TotalCost");
        }
    }
}
