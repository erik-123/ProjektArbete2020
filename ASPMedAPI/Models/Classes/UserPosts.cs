using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPMedAPI.Models.Classes
{
        public class UserPost
        {
            public int Id { get; set; }

            public string Message { get; set; }

            public DateTime Date { get; set; }

            public virtual ApplicationUser Sender { get; set; }

            public virtual ApplicationUser Receiver { get; set; }
        }
 

}

