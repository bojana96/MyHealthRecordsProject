using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthRecords.Models
{
    public class DoctorPatientDiagnose
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public String Date { get; set; }
        public String Description { get; set; }
    }
}