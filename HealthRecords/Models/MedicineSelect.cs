using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HealthRecords.Models
{
    public class MedicineSelect
    {
        public IEnumerable<Medicine> Medicines { get; set; }
        public IEnumerable<Medicine> SelectedMedicines { get; set; }

        public MedicineSelect()
        {

        }
    }
}