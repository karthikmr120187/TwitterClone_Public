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
    public class FOLLOWINGController : Controller
    {
        private TwitterDBEntities db = new TwitterDBEntities();

        // GET: FOLLOWING
        public ActionResult Index()
        {
            //var following = db.People.Include(f => f.FOLLOWINGs).ToList();
            //db.FOLLOWINGs.Include().ToList();
            FOLLOWING flw = new FOLLOWING()
            {
                PERSON=db.People.ToList()
                
                
            };
            var user = Session["user"].ToString();
            ViewBag.follower = db.FOLLOWINGs.Where(p => p.user_id == user).Select(p => p.following_id).ToList();
            
            var listflw= new SelectList(db.FOLLOWINGs, "following_id", flw.following_id);
            if(listflw==null )
            {
                ViewBag.Count = 0;
            }
            return View(flw);
        }

        // GET: FOLLOWING/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOLLOWING fOLLOWING = db.FOLLOWINGs.Find(id);
            if (fOLLOWING == null)
            {
                return HttpNotFound();
            }
            return View(fOLLOWING);
        }

        //GET: FOLLOWING/Create
        public ActionResult Create(string userid)
        {
            //ViewBag.user_id = new SelectList(db.People, "user_id");
            ViewBag.user_id = userid;
            Session["clckuser"] = userid;
            return View();
        }

        // POST: FOLLOWING/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,following_id")] FOLLOWING fOLLOWING)
        {
            if (ModelState.IsValid)
            {
                fOLLOWING.user_id = Session["user"].ToString();
                fOLLOWING.following_id = Session["clckuser"].ToString();
                db.FOLLOWINGs.Add(fOLLOWING);
                //db.Entry(fOLLOWING).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.user_id = new SelectList(db.People, "user_id", fOLLOWING.user_id);
            return View(fOLLOWING);
        }

        // GET: FOLLOWING/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOLLOWING fOLLOWING = db.FOLLOWINGs.Find(id);
            if (fOLLOWING == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.People, "user_id", "password", fOLLOWING.user_id);
            return View(fOLLOWING);
        }

        // POST: FOLLOWING/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,following_id")] FOLLOWING fOLLOWING)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fOLLOWING).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.People, "user_id", "password", fOLLOWING.user_id);
            return View(fOLLOWING);
        }

        // GET: FOLLOWING/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOLLOWING fOLLOWING = db.FOLLOWINGs.Find(id);
            if (fOLLOWING == null)
            {
                return HttpNotFound();
            }
            return View(fOLLOWING);
        }

        // POST: FOLLOWING/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            FOLLOWING fOLLOWING = db.FOLLOWINGs.Find(id);
            db.FOLLOWINGs.Remove(fOLLOWING);
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
