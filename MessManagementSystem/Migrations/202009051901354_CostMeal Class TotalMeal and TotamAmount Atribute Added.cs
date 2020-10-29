namespace MessManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CostMealClassTotalMealandTotamAmountAtributeAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CostMeals", "TotalMeal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CostMeals", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.CostMeals", "MealEntryId");
            DropColumn("dbo.CostMeals", "PaymentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CostMeals", "PaymentId", c => c.Int(nullable: false));
            AddColumn("dbo.CostMeals", "MealEntryId", c => c.Int(nullable: false));
            DropColumn("dbo.CostMeals", "TotalAmount");
            DropColumn("dbo.CostMeals", "TotalMeal");
        }
    }
}
