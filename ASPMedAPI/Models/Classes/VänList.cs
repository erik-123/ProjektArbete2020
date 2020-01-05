using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPMedAPI.Models.Classes
{
    public class VänModel
    {
        public List<Vän> Vän { get; set; }
    }
    public class VänListItem
    {
        public string UserId { get; set; }
        public string Förnamn { get; set; }
        public string Efternamn { get; set; }
    }

    public class FriendLists
    {
        public List<VänListItem> VänListItem { get; set; }
    }
}