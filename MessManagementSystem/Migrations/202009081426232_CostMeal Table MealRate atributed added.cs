namespace MessManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CostMealTableMealRateatributedadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CostMeals", "MealRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CostMeals", "MealRate");
        }
    }
}
