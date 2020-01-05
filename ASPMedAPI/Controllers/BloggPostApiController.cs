using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ASPMedAPI.Models;
using ASPMedAPI.Models.Classes;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ASPMedAPI.Controllers
{
    [RoutePrefix("api/bloggpost")]
    public class BloggPostApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        [Route("add")]
        public void PushEntry(BloggPost model)
        {
            model.SkickatDatum = DateTimeOffset.Now;
            var db = new ApplicationDbContext();
            db.BloggPosts.Add(model);
            db.SaveChanges();
        }


        [HttpGet]
        [Route("list")]
        public List<BloggPostViewModel> GetMessages(string userId)
        {
            var db = new ApplicationDbContext();
            var messages = db.BloggPosts.Where(m => m.Mottagare == userId);

            var list = messages.Join(db.Profil, m => m.Avsändare, p => p.UserID, (m, p) => new { m, p })

                .Select(a =>
                    new BloggPostViewModel
                    {
                        Post_Id = a.m.Post_Id,
                        MeddelandeText = a.m.MeddelandeText,
                        Mottagare = a.m.Mottagare,
                        FulltNamn = a.p.Förnamn + " " + a.p.Efternamn,
                        SkickatDatum = a.m.SkickatDatum
                    }
                ).ToList();

            return list;
        }

        public void AddPost([FromBody] BloggPostViewModel model)
        {

            if (model.MeddelandeText != "")
            {
                UserPost newPost = new UserPost();
                var userTo = db.Users.Single(x => x.Id == model.ReceiverID);
                newPost.Receiver = userTo;

                var userFrom = db.Users.Single(x => x.Id == model.SenderID);
                newPost.Sender = userFrom;

                newPost.Message = model.MeddelandeText;

                newPost.Date = DateTime.Now;

                db.Posts.Add(newPost);
                db.SaveChanges();

            }

        }

        public List<UserPost> GetPosts(string id)
        {
            var Posts = db.Posts.Where(x => x.Receiver.Id == id).ToList();
            return Posts;
        }

    }
}



