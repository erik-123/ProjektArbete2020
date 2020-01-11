using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPMedAPI.Models;
using Microsoft.AspNet.Identity;

namespace ASPMedAPI.App_Start
{
    public class VänFörgåningarCounter : ActionFilterAttribute
    {
        // GET: VänFörgåningarCounter
        public VänFörgåningarCounter()
        {
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            
            //Om användaren är inloggad är det sant, annars falskt
            if (filterContext.HttpContext.User?.Identity.IsAuthenticated ?? false) 
            {
                //Sätter IncomingRequestCount till antalet vänförfrågningar användaren har
                var db = new ApplicationDbContext();
                
                var currentID = filterContext.HttpContext.User.Identity.GetUserId();
                var incommingRequests = db.VänFörfrågningar.Where(f => f.Person2 == currentID);
                var req = incommingRequests.Count();
                filterContext.Controller.ViewData["IncomingRequestsCount"] = req;
            }
        }
    }
}