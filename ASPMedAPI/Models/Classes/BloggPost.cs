using System;
using System.ComponentModel.DataAnnotations;


namespace ASPMedAPI.Models.Classes
{
    public class BloggPost
    {
        [Key]
        public int Post_Id { get; set; }
        public string Avsändare { get; set; }
        public string Mottagare { get; set; }

        public DateTimeOffset SkickatDatum { get; set; }
        public string MeddelandeText { get; set; }
    }
}