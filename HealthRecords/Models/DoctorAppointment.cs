using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthRecords.Models
{
    public class DoctorAppointment
    {
        public List<Doctor> Doctors { get; set; }
        public int selectedDoctorId { get; set; }
        public int AppointmentId { get; set; } 
    }
}