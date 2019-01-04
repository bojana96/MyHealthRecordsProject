using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthRecords.Models
{
    public class Diagnose
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression("(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))-(((0[1-9])|(1[0-2]))|([1-9]))-(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)", ErrorMessage = "Date format is not valid!")]
        public String Date { get; set; }
        [Required]
        public String Description { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}