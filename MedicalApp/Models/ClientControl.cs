using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Models
{
    public class ClientControl
    {
        public Guid ControlID { get; set; }
        public int ClientID { get; set; }
        public string ControlText { get; set; }
        public string ControlDate { get; set; }
    }
}
