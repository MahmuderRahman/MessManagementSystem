namespace MessManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberCosttableadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberCosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        CalculationId = c.Int(nullable: false),
                        TotalDeposit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalMeal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MemberCosts");
        }
    }
}
