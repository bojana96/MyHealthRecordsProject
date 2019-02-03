namespace HealthRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class it : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Appointment_AppointmentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Doctors", "Appointment_AppointmentId");
            AddForeignKey("dbo.Doctors", "Appointment_AppointmentId", "dbo.Appointments", "AppointmentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "Appointment_AppointmentId", "dbo.Appointments");
            DropIndex("dbo.Doctors", new[] { "Appointment_AppointmentId" });
            DropColumn("dbo.Doctors", "Appointment_AppointmentId");
        }
    }
}
