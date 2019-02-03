namespace HealthRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kafe : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Doctors", "DoctorAppointment_AppointmentId", "dbo.DoctorAppointments");
            DropIndex("dbo.Doctors", new[] { "DoctorAppointment_AppointmentId" });
            DropColumn("dbo.Doctors", "DoctorAppointment_AppointmentId");
            DropTable("dbo.DoctorAppointments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DoctorAppointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        selectedDoctorId = c.Int(nullable: false),
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
                        patientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentId);
            
            AddColumn("dbo.Doctors", "DoctorAppointment_AppointmentId", c => c.Int());
            CreateIndex("dbo.Doctors", "DoctorAppointment_AppointmentId");
            AddForeignKey("dbo.Doctors", "DoctorAppointment_AppointmentId", "dbo.DoctorAppointments", "AppointmentId");
        }
    }
}
