using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ASPMedAPI.Models.Classes
{
    public class VänFörfrågan
    {
        [Key]
        public int Vän_ID { get; set; }
        public string Person1 { get; set; }
        public string Person2 { get; set; }
    }
}