using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Models
{
    public class Medicine
    {
        public int MedicineID { get; set; }
        public int ClientID { get; set; }
        public string MedicineName { get; set; }
        public string MedicineDose { get; set; }
    }
}
