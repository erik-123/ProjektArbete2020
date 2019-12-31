using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMedAPI.Models.Classes
{
    public class Image
    {

        [Key]
        public int Image_ID{ get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        

    }
}