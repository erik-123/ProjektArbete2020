using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMedAPI.Models
{
    public class ProfilViewModel
    {
        public string UserID { get; set; }
        public string Förnamn { get; set; }
        public string Efternamn { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FödelseDatum { get; set; }
        public string ProfileURL { get; set; }
        public string Bio { get; set; }
        
    }

    public class ProfileUpdateViewModel
    {

        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [StringLength(60, MinimumLength = 3)]
        public string Firstname { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [StringLength(60, MinimumLength = 3)]
        public string Lastname { get; set; }


        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }


        [StringLength(300, MinimumLength = 3)]
        public string Description { get; set; }

        public string ImageName { get; set; }

    }
}