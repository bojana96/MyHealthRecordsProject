using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthRecords.Models
{
    public class MedicineSelect
    {
        public List<Medicine> Medicines { get; set; }
        public List<Medicine> SelectedMedicines { get; set; }

        public MedicineSelect()
        {

        }
    }
}