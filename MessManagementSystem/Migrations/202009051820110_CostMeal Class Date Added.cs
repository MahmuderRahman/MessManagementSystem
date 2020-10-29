namespace MessManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CostMealClassDateAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CostMeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MonthId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        MealEntryId = c.Int(nullable: false),
                        PaymentId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CostMeals");
        }
    }
}
