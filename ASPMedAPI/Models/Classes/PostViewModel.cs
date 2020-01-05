using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMedAPI.Models.Classes
{
    public class PostViewModel
        {
            public string SenderID { get; set; }
            public string ReceiverID { get; set; }
            [StringLength(500), Required]
            public string Text { get; set; }

        }
    
}
