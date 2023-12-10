using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Models
{
    public class Operation
    {
        public int OperationID { get; set; }
        public int ClientID { get; set; }
        public string OperationName { get; set; }
        public string OperationDate { get; set; }
    }
}
