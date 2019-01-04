using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthRecords.Models
{
    public class AddUserToRole
    {
        public string Email { get; set; }
        public List<String> Roles { get; set; }
        public string selectedRole { get; set; }
    }
}