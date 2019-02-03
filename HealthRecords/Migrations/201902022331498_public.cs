namespace HealthRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _public : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Embg = c.String(),
                        Phone = c.String(),
                        Gender = c.String(),
                        Insurance = c.Boolean(nullable: false),
                        BirthDate = c.String(),
                        Condition = c.String(),
                    })
                .PrimaryKey(t => t.AppointmentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Appointments");
        }
    }
}
