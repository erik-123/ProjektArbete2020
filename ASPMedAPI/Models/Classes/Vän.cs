using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ASPMedAPI.Models.Classes
{
    public class Vän
    {
        
           [Key]
            public int Vän_ID { get; set; }
            public string Person1 { get; set; }
            public string Person2 { get; set; }
          
        
    }
}
