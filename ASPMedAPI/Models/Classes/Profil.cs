using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPMedAPI.Models.Classes
{
    public class Profil
    {
           [Key]
            public string UserID { get; set; }
            public string Förnamn { get; set; }
            public string Efternamn { get; set; }
            public DateTime Födelsedatum { get; set; }
            public string ProfileURL { get; set; }
            public string Bio { get; set; }

             public bool aktiv { get; set; }

        
    }
}