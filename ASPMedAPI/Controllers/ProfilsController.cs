using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPMedAPI.Models;
using ASPMedAPI.Models.Classes;
using Microsoft.AspNet.Identity;
using System.IO;

namespace ASPMedAPI.Controllers
{
    public class ProfilsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profils
        public ActionResult Index()
        {
            return View(db.Profil.ToList());
        }

        public ActionResult ShowProfile(string showID)
        {

            var db = new ApplicationDbContext();
            var userInfo = db.Profil.FirstOrDefault(p => p.UserID == showID);

            if (userInfo == null)
            {
                return RedirectToAction("Error", "Profile");
            }
            else
            {
                return View(new ProfilViewModel
                {
                    UserID = userInfo.UserID,
                    Förnamn = userInfo.Förnamn,
                    Efternamn = userInfo.Efternamn,
                    FödelseDatum = userInfo.Födelsedatum,
                    ProfileURL = userInfo.ProfileURL,


                });

                //return View(db.Profil.ToList());
            }
        }


        // GET: Profils/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profil profil = db.Profil.Find(id);
            if (profil == null)
            {
                return HttpNotFound();
            }
            return View(profil);
        }

        // GET: Profils/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profils/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Förnamn,Efternamn,Födelsedatum,ProfileURL,Bio")]HttpPostedFileBase file, Profil profil)
        {
            try
            {
                if (ModelState.IsValid) {

                    try
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
                            file.SaveAs(path);
                            ViewBag.FileStatus = "File uploaded successfully.";
                            ViewBag.Message = "File uploaded successfully.";
                            db.Profil.Add(profil);
                            db.SaveChanges();
                            return RedirectToAction("Index");

                        }

                    }
                    catch (Exception ex)
                    {
                        ViewBag.FileStatus = "Error while file uploading.";
                        ViewBag.Message = "Error" + ex.Message.ToString();
                    }
                }
            }

            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
            return View(profil);
        }

        //if (ModelState.IsValid)
        //{
        //    db.Profil.Add(profil);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        // }

        // GET: Profils/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profil profil = db.Profil.Find(id);
            if (profil == null)
            {
                return HttpNotFound();
            }
            return View(profil);
        }

        // POST: Profils/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Förnamn,Efternamn,Födelsedatum,ProfileURL,Bio")] Profil profil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profil);
        }

        // GET: Profils/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profil profil = db.Profil.Find(id);
            if (profil == null)
            {
                return HttpNotFound();
            }
            return View(profil);
        }

        // POST: Profils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Profil profil = db.Profil.Find(id);
            db.Profil.Remove(profil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Search(string förnamn, string efternamn)
        {
            ViewBag.Message = "Search page.";

            var currentUserID = User.Identity.GetUserId();
            var Profiles = new ApplicationDbContext().Profil.Where(
                    (s =>
                    (s.Förnamn.Contains(förnamn) || förnamn == null && !(s.Förnamn == "Förnamn")) &&
                    (s.Efternamn.Contains(efternamn) || efternamn == null && !(s.Efternamn == "Efternamn"))
                     &&
                    !(s.UserID.Equals(currentUserID))));
            return View(Profiles);
        }

        public ActionResult ChangeProfileImage(HttpPostedFileBase File)
        {

            if (File != null && File.ContentLength > 0)
            {

                var NoExtension = Path.GetFileNameWithoutExtension(File.FileName);
                var Extension = Path.GetExtension(File.FileName);

                var NameOfFile = NoExtension + DateTime.Now.ToString("yyyy-MM-dd-fff") + Extension;

                var NameOfPath = "/Images/" + NameOfFile;
                string FilePath = Path.Combine(Server.MapPath("~/Images/"), NameOfFile);

                File.SaveAs(FilePath);

                var db = new ApplicationDbContext();
                var userId = User.Identity.GetUserId();
                var currentProfile =
                    db.Profil.FirstOrDefault(p => p.UserID == userId);

                currentProfile.ProfileURL = NameOfPath;
                db.SaveChanges();

                return RedirectToAction("ShowProfile", "Profils", new { showID = userId });

            }
            else
            {
                return RedirectToAction("Error", "Profils");
            }
        }
        
        [HttpPost]
        public ActionResult AddImage(ProfileUpdateViewModel model, HttpPostedFileBase file)
        {
            var db = new ApplicationDbContext();
            var currentUser = User.Identity.GetUserId();
            var currentProfile = db.Profil.FirstOrDefault(x => x.UserID == currentUser);

            if (file.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/images"), _FileName);
                file.SaveAs(_path);
                var imgNameToSave = "/images/" + _FileName;
                db.Profil.FirstOrDefault(p => p.UserID == currentUser).ProfileURL = imgNameToSave;

            }
            else
            {
                currentProfile.ProfileURL = model.ImageName;
            }
            db.SaveChanges();
            return View("~/Views/Profils/AddImage.cshtml");
            //return RedirectToAction("ShowProfile", "Profils");
        }

        public ActionResult AddImage()
        {
            return View();

        }

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);

                    }
                    ViewBag.FileStatus = "File uploaded succefully.";

                }
                catch (Exception) { ViewBag.FileStatus = "Error while uploadning"; }
            }
            return View("AddImage");
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
