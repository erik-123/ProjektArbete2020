using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPMedAPI.Models.Classes
{
    public class BloggPostViewModel
    {
        public int Post_Id { get; set; }
        public string FulltNamn { get; set; }
        public string Mottagare { get; set; }

        public string SenderID { get; set; }
        public string ReceiverID { get; set; }

        public DateTimeOffset SkickatDatum { get; set; }
        public string MeddelandeText { get; set; }

    }
}