namespace HealthRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmbgToAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Embg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Embg");
        }
    }
}
