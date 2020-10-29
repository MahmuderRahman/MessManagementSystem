namespace MessManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MonthlyCalculationtableadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MonthlyCalculations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MonthId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        TotalMeal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MealRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MonthlyCalculations");
        }
    }
}
