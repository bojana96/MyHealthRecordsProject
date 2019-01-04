using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthRecords.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Surname { get; set; }
        [Required]
        [Range(18, 65, ErrorMessage ="Age must be between 18 and 65")]
        public int Age { get; set; }
        [Required]
        public String Embg { get; set; }
        [Required]
        public String Address { get; set; }
        public List<Patient> Patients { get; set; }
    }
}