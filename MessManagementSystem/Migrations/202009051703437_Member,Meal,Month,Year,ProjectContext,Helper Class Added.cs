namespace MessManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberMealMonthYearProjectContextHelperClassAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MealEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        Breakfast = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Lunch = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Dinner = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalMeal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MemberEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        ContactNo = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Months",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Years",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Years");
            DropTable("dbo.Months");
            DropTable("dbo.MemberEntries");
            DropTable("dbo.MealEntries");
        }
    }
}
