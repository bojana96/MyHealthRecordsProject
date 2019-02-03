namespace HealthRecords.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppointmentUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "doctor_Id", c => c.Int());
            AddColumn("dbo.Appointments", "patient_Id", c => c.Int());
            CreateIndex("dbo.Appointments", "doctor_Id");
            CreateIndex("dbo.Appointments", "patient_Id");
            AddForeignKey("dbo.Appointments", "doctor_Id", "dbo.Doctors", "Id");
            AddForeignKey("dbo.Appointments", "patient_Id", "dbo.Patients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "patient_Id", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "doctor_Id", "dbo.Doctors");
            DropIndex("dbo.Appointments", new[] { "patient_Id" });
            DropIndex("dbo.Appointments", new[] { "doctor_Id" });
            DropColumn("dbo.Appointments", "patient_Id");
            DropColumn("dbo.Appointments", "doctor_Id");
        }
    }
}
