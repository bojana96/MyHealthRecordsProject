namespace HealthRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class item : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        ItemId = c.String(nullable: false, maxLength: 128),
                        CartId = c.String(),
                        Quantity = c.Int(nullable: false),
                        medicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Medicines", t => t.medicineId, cascadeDelete: true)
                .Index(t => t.medicineId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "medicineId", "dbo.Medicines");
            DropIndex("dbo.CartItems", new[] { "medicineId" });
            DropTable("dbo.CartItems");
        }
    }
}
