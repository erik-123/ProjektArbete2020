using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPMedAPI.Models.Classes
{
    public class VänFörfråganViewModel
    {
    }
    public class RequestSent
    {
        public bool Success { get; set; }
        public string Error { get; set; }
    }
    public class FriendRequestList
    {
        public int FriendRequest_Id { get; set; }
        public string UserId { get; set; }
        public string Förnamn { get; set; }
        public string Efternamn { get; set; }

    }
    public class RequestList
    {
        public List<FriendRequestList> RequestLists { get; set; }
    }
    public class PendingRequests
    {
        public int Outgoing { get; set; }
        public int Incomming { get; set; }
    }

}