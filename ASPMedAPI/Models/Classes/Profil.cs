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

        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string ProfileURL { get; set; }
            public string Bio { get; set; }

        
    }
}