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
using System.IO;

namespace ASPMedAPI.Controllers
{
    public class ImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Images
        
        public ActionResult Index(int id)
        { 
            //Image image = new Image();
            //Image image = db.Images.Find(id);
            
            //    image = db.Images.Where(x => x.ImageID == id).FirstOrDefault();
            

            return View(db.Images.ToList());

                     
        
           
         
            //return View(image);
        





    }

        // GET: Images/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            //    Image image = new Image();

            //        image = db.Images.Where(x => x.ImageID == id).FirstOrDefault();

            //    return View(image);
            //}
            return View(db.Images.ToList());
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Image image = db.Images.Find(id);
            //if (image == null)
            //{
            //    return HttpNotFound();
            //}*
            //return View(image);
            }

            // GET: Images/Create
            public ActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "Image_ID,Title,ImagePath")]*/ Image image)
        {
            string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extension = Path.GetExtension(image.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            image.ImagePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            image.ImageFile.SaveAs(fileName);


            //var pdb = new ApplicationDbContext();

            //var userId = User.Identity.GetUserId();
            //var profile = pdb.Images.FirstOrDefault(x => x.Image_ID == id);
            //profile.ImagePath = fileName;
            //pdb.SaveChanges();



            //var currentProfile =
            //    pdb.Profiles.FirstOrDefault(p => p.UserID == userId);
            

            //var pdb = new ProfileDbContext();
            //var userId = User.Identity.GetUserId();
            //var currentProfile =
                //pdb.Profiles.FirstOrDefault(p => p.UserID == userId);
            //Bildens sökväg sparas i databasen
            //currentProfile.ProfileURL = NameOfPath;
            //pdb.SaveChanges();

            //return RedirectToAction("ShowProfile", "Profile", new { showID = userId });

        //}
          






            //if (ModelState.IsValid)
            //{
            //    db.Images.Add(image);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            return View(image);
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Image_ID,Title,ImagePath")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(image);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
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
