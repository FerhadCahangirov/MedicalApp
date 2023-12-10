using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Models
{
    public class Client
    {
        public Guid ClientID { get; set; }
        public string ClientFullName { get; set; }
        public string ClientBirthDate { get; set; }
        public bool ClientGender { get; set; } 
        public string ClientPhoneNumber { get; set; }
        public string ClientComplaint { get; set; }
        public string ClientHarmfullHabitats { get; set; }
        public string ClientAdviceText { get; set; }
        public string ClientApplyDate { get; set; }
    }
}
