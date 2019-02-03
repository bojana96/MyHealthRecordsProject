namespace HealthRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creating : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "MedicineId", "dbo.Medicines");
            DropIndex("dbo.CartItems", new[] { "MedicineId" });
            DropTable("dbo.CartItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        ItemId = c.String(nullable: false, maxLength: 128),
                        CartId = c.String(),
                        Quantity = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId);
            
            CreateIndex("dbo.CartItems", "MedicineId");
            AddForeignKey("dbo.CartItems", "MedicineId", "dbo.Medicines", "Id", cascadeDelete: true);
        }
    }
}
