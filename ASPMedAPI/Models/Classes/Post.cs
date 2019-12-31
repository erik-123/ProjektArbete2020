using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMedAPI.Models.Classes
{
    public class Post
    {
        [Key]
        public int post_id { get; set;}
        public string post_txt { get; set; }
        public DateTime post_date { get; set; }
        public int post_like { get; set; }

    }
}