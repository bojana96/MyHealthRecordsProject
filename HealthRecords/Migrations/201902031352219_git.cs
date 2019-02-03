namespace HealthRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class git : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Doctors", "Appointment_AppointmentId", "dbo.Appointments");
            DropIndex("dbo.Doctors", new[] { "Appointment_AppointmentId" });
            AddColumn("dbo.DoctorAppointments", "patientId", c => c.Int(nullable: false));
            DropColumn("dbo.Doctors", "Appointment_AppointmentId");
            DropTable("dbo.Appointments");
        }
        
        public override void Down()
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
            
            AddColumn("dbo.Doctors", "Appointment_AppointmentId", c => c.String(maxLength: 128));
            DropColumn("dbo.DoctorAppointments", "patientId");
            CreateIndex("dbo.Doctors", "Appointment_AppointmentId");
            AddForeignKey("dbo.Doctors", "Appointment_AppointmentId", "dbo.Appointments", "AppointmentId");
        }
    }
}
