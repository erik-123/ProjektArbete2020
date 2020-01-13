using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPMedAPI.Models;
using ASPMedAPI.Models.Classes;
using Microsoft.AspNet.Identity;

namespace ASPMedAPI.Controllers
{
    [Authorize]
    public class VänController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vän
        public ActionResult StartIndex()
        {
            var db = new ApplicationDbContext();
            var currentID = User.Identity.GetUserId();
            var incommingRequests = db.VänFörfrågningar.Where(f => f.Person2 == currentID);
            var outgoingrequests = db.VänFörfrågningar.Where(f => f.Person1 == currentID);
            var pending = new PendingRequests { Incomming = incommingRequests.Count(), Outgoing = outgoingrequests.Count() };

            return View(pending);
        }
        public ActionResult Index()
        {
            return View(db.Vän.ToList());
        }
        [HttpGet]
        public ActionResult RequestList(string friendID)
        {
            var db = new ApplicationDbContext();
            var currentID = User.Identity.GetUserId();

            var friendExist = db.Profil.Any(p => p.UserID == friendID);
            if ((!(friendID == null)) && friendExist)
            {

                if (!(db.VänFörfrågningar.Any(f =>
                (f.Person1 == currentID && f.Person2 == friendID) ||
                (f.Person1 == friendID && f.Person2 == currentID)
                )))
                {

                    if (!(db.Vän.Any(f =>
                     (f.Person1 == currentID && f.Person2 == friendID) ||
                     (f.Person1 == friendID && f.Person2 == currentID))))
                    {

                        db.VänFörfrågningar.Add(new VänFörfrågan
                        {
                            Person1 = currentID,
                            Person2 = friendID
                        });

                        db.SaveChanges();
                        return View(new RequestSent { Success = true });
                    }
                    else
                    {
                        return View(new RequestSent { Success = false, Error = "You are already friends" });
                    }
                }
                else
                {
                    return View(new RequestSent { Success = false, Error = "Request is already sent" });
                }
            }
            else
            {
                return View(new RequestSent { Success = false, Error = "Something went wrong, please try again" });
            }
        }

        public ActionResult IncommingList()
        {
            var db = new ApplicationDbContext();
            var currentID = User.Identity.GetUserId();
            var listOfRequests = db.VänFörfrågningar.Where(f => f.Person2 == currentID);
            var listOfProfiles = db.Profil.ToList();
            List<FriendRequestList> listToSend = new List<FriendRequestList>();



            foreach (var u in listOfRequests)
            {
                var AddId = u.Person1;
                var User = listOfProfiles.FirstOrDefault(p => p.UserID == AddId);

                var AddFornamn = User.Förnamn;
                var AddEfternamn = User.Efternamn;

                var AddThis = new FriendRequestList
                {
                    FriendRequest_Id = u.Vän_ID,
                    UserId = AddId,
                    Förnamn = AddFornamn,
                    Efternamn = AddEfternamn
                };
                listToSend.Add(AddThis);
            }

            if (listToSend.Any())
            {
                return View(listToSend);
            }
            else
            {
                return View();
            }
        }
        public ActionResult OutgoingList()
        {
            var db = new ApplicationDbContext();
            var currentID = User.Identity.GetUserId();
            var listOfRequests = db.VänFörfrågningar.Where(f => f.Person1 == currentID);
            var listOfProfiles = db.Profil.ToList();


            List<FriendRequestList> listToSend = new List<FriendRequestList>();



            foreach (var u in listOfRequests)
            {
                var AddId = u.Person2;
                var User = listOfProfiles.FirstOrDefault(p => p.UserID == AddId);

                var AddFörnamn = User.Förnamn;
                var AddEfternamn = User.Efternamn;

                var AddThis = new FriendRequestList
                {
                    FriendRequest_Id = u.Vän_ID,
                    UserId = AddId,
                    Förnamn = AddFörnamn,
                    Efternamn = AddEfternamn
                };
                listToSend.Add(AddThis);
            }

            if (listToSend.Any())
            {
                return View(listToSend);
            }
            else
            {
                return View();
            }
        }

        public ActionResult RemoveRequest(int removeID)
        {
            var db = new ApplicationDbContext();
            var remove = db.VänFörfrågningar.FirstOrDefault(f => f.Vän_ID == removeID);
            db.VänFörfrågningar.Remove(remove);
            db.SaveChanges();

            return RedirectToAction("OutgoingList");
        }

        public ActionResult RequestRespone(int requestID, bool acceptRequest)
        {
            var db = new ApplicationDbContext();
            var friendRequest = db.VänFörfrågningar.FirstOrDefault(f => f.Vän_ID == requestID);


            if (acceptRequest)
            {
                var addFriend = new Vän { Person1 = friendRequest.Person1, Person2 = friendRequest.Person2 };
                db.Vän.Add(addFriend);
                db.SaveChanges();
            }

            var remove = db.VänFörfrågningar.FirstOrDefault(f => f.Vän_ID == requestID);
            db.VänFörfrågningar.Remove(remove);
            db.SaveChanges();

            return RedirectToAction("IncommingList");
        }

        public ActionResult FriendList()
        {
            var db = new ApplicationDbContext();
            var currentID = User.Identity.GetUserId();
            var friendList = db.Vän.Where(f => f.Person1 == currentID || f.Person2 == currentID);
            var listOfProfiles = db.Profil.ToList();
            List<VänListItem> listToSend = new List<VänListItem>();


            foreach (var f in friendList)
            {
                var profile = listOfProfiles.FirstOrDefault(p => p.UserID != currentID && (p.UserID == f.Person1 || p.UserID == f.Person2));
                var friend = new VänListItem
                {
                    UserId = profile.UserID,
                    Förnamn = profile.Förnamn,
                    Efternamn = profile.Efternamn
                };
                listToSend.Add(friend);
            }

            return View(listToSend);
        }

        public int CountRequest()
        {
            var count = -2;
           var db = new ApplicationDbContext();
           var userID = User.Identity.GetUserId();
            var request = db.VänFörfrågningar.Where(f => f.Person2== userID).ToList();
            var profiles = db.Profil.ToList();
       
            count = request.Count();

            foreach (var f in request)
            {

                if (f.Vän_ID >=1)
                {
                   count++;
                }
            }

            return count;
        }

        public ActionResult RemoveFriend(string friendID)
        {
            var db = new ApplicationDbContext();
            var currentID = User.Identity.GetUserId();
            var remove = db.Vän.FirstOrDefault(f => f.Person1 == friendID && f.Person2 == currentID || f.Person1 == currentID && f.Person2 == friendID);
            db.Vän.Remove(remove);
            db.SaveChanges();

            return RedirectToAction("FriendList");
        }


        // GET: Vän/Details/5
        public ActionResult Lista()
        {

            {
                var db = new ApplicationDbContext();
                var NuvarandeID = User.Identity.GetUserId();
                var VänLista = db.Vän.Where(f => f.Person1 == NuvarandeID || f.Person2 == NuvarandeID);
                var listOfProfiles = db.Profil.ToList();
                List<VänListItem> listaAttSkicka = new List<VänListItem>();

                foreach (var f in VänLista)
                {
                    var profile = listOfProfiles.FirstOrDefault(x => x.UserID != NuvarandeID && (x.UserID == f.Person1 || x.UserID == f.Person2));
                    var vän = new VänListItem
                    {
                        UserId = profile.UserID,
                        Förnamn = profile.Förnamn,
                        Efternamn = profile.Efternamn
                    };
                    listaAttSkicka.Add(vän);
                }
                return View(listaAttSkicka);

            }
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vän vän = db.Vän.Find(id);
            if (vän == null)
            {
                return HttpNotFound();
            }
            return View(vän);
        }

        // GET: Vän/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vän/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Vän_ID,Person1,Person2")] Vän vän)
        {
            if (ModelState.IsValid)
            {
                db.Vän.Add(vän);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vän);
        }

        // GET: Vän/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vän vän = db.Vän.Find(id);
            if (vän == null)
            {
                return HttpNotFound();
            }
            return View(vän);
        }

        // POST: Vän/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Vän_ID,Person1,Person2")] Vän vän)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vän).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vän);
        }

        // GET: Vän/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vän vän = db.Vän.Find(id);
            if (vän == null)
            {
                return HttpNotFound();
            }
            return View(vän);
        }

        // POST: Vän/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vän vän = db.Vän.Find(id);
            db.Vän.Remove(vän);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

