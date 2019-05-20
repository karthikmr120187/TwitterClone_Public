using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class PeopleController : Controller
    {
        private TwitterDBEntities db = new TwitterDBEntities();

        // GET: People
        public ActionResult Index()
        {
            ViewBag.UserId = Session["user"];

            PERSON ppl = new PERSON()
            {
                TWEETs = db.TWEETs.ToList(),
                FOLLOWINGs = db.FOLLOWINGs.ToList(),
                peopleData=db.People.ToList()
                
            };
            
           
            return View(ppl);
        }

        // GET: People/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSON pERSON = db.People.Find(id);
            if (pERSON == null)
            {
                return HttpNotFound();
            }
            return View(pERSON);
        }

        
        // GET: People/Edit/5
        public ActionResult EditTweet(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSON pERSON = new PERSON()
            {
                TWEETs = db.TWEETs.Where(s => s.tweet_id == id).Select(s => s).ToList()
            };


            if (pERSON == null)
            {
                return HttpNotFound();
            }
            return View("UpdateTweet", pERSON);
        }

        [HttpPost]
        [Route("People/PostTweet")]
        public ActionResult PostTweet(PERSON pERSON)
        {
            if (ModelState.IsValid)


            {
                pERSON.TweetData.created = DateTime.Now;
                pERSON.TweetData.user_id = Session["user"].ToString();
                db.TWEETs.Add(pERSON.TweetData);
                db.SaveChanges();
            }

            return View("Index");
        }
        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,password,fullName,email,joined,active")] PERSON pERSON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pERSON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pERSON);
        }


        [HttpPost]
        [Route("People/UpdateTweet")]
        public ActionResult UpdateTweet(PERSON pERSON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pERSON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pERSON);
        }
        // GET: People/Delete/5
        public ActionResult DeleteTweet(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSON pERSON = db.People.Find(id);
            if (pERSON == null)
            {
                return HttpNotFound();
            }
            return View(pERSON);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PERSON pERSON = db.People.Find(id);
            db.People.Remove(pERSON);
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
