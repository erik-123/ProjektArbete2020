using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Web;

//namespace ASPMedAPI.Models.Classes
//{
//    public class Meddelande
//    {
//            [Key]
//            public int Id { get; set; }
//            public string MeddelandeText { get; set; }
//            public DateTime Timestamp { get; set; }

//            [ForeignKey("User")]
//            public string UserId { get; set; }
//            public ApplicationUser User { get; set; }

//            public Meddelande() { }
//            public Meddelande(MeddelandeDto meddelandeDto)
//            {
//                MeddelandeText = meddelandeDto.MeddelandeText;
//                Timestamp = DateTime.Parse(meddelandeDto.Timestamp);
//                UserId = meddelandeDto.UserId;
//            }
//        }

//        public class MeddelandeDto
//        {
//            public string MeddelandeText { get; set; }
//            public string Timestamp { get; set; }
//            public string UserId { get; set; }
//            public string Användarnamn { get; set; }

//        public  MeddelandeDto() { }
//            public MeddelandeDto (Meddelande meddelande)
//            {
//                MeddelandeText = meddelande.MeddelandeText;
//                Timestamp = meddelande.Timestamp.ToString(@"yyyy-MM-dd HH\:mm\:ss");
//                UserId = meddelande.UserId;
//                // Använd profilens namn som användarnamn:
//                Användarnamn = meddelande.User?.Profil?.DisplayName ?? meddelande.User?.UserName;
//            }
//        }
//    }
//}