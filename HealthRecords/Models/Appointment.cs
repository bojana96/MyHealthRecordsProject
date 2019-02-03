using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthRecords.Models
{
    public class Appointment
    {
        [Key]
       public String AppointmentId { get; set; }
       public String FirstName { get; set; }
        public String LastName { get; set; }
        public List<Doctor> Doctors { get; set; } 
        public String Address { get; set; }
        public  String City { get; set; }
        public String Embg { get; set; }
        public String Phone { get; set; }
        public  String Gender { get; set; }
        public Boolean Insurance { get; set; }
        public String BirthDate { get; set; }
        public String Condition { get; set; }
    }
}