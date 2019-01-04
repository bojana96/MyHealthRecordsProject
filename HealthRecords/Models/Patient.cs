using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthRecords.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Surname { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public String Address { get; set; }
        [Required]
        public String Embg { get; set; }
        public Doctor Doctor { get; set; }
    }
}