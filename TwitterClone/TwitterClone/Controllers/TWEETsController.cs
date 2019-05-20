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
    public class TWEETsController : Controller
    {
        private TwitterDBEntities db = new TwitterDBEntities();

        // GET: TWEETs
        public ActionResult Index()
        {
            var tWEETs = db.TWEETs.Include(t => t.PERSON);
            return View(tWEETs.ToList());
        }

        // GET: TWEETs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TWEET tWEET = db.TWEETs.Find(id);
            if (tWEET == null)
            {
                return HttpNotFound();
            }
            return View(tWEET);
        }

        // GET: TWEETs/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.People, "user_id", "password");
            return View();
        }

        // POST: TWEETs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TWEET tWEET)
        {
            if (ModelState.IsValid)
            {
                tWEET.user_id = Session["user"].ToString();
                tWEET.created = DateTime.Now;
                db.TWEETs.Add(tWEET);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.People, "user_id", "password", tWEET.user_id);
            return View(tWEET);
        }

        // GET: TWEETs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TWEET tWEET = db.TWEETs.Find(id);
            if (tWEET == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.People, "user_id", "password", tWEET.user_id);
            return View(tWEET);
        }

        // POST: TWEETs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tweet_id,user_id,message,created")] TWEET tWEET)
        {
            if (ModelState.IsValid)
            {
                tWEET.user_id = Session["user"].ToString();
                tWEET.created = DateTime.Now;

                db.Entry(tWEET).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.People, "user_id", "password", tWEET.user_id);
            return View(tWEET);
        }

        // GET: TWEETs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TWEET tWEET = db.TWEETs.Find(id);
            if (tWEET == null)
            {
                return HttpNotFound();
            }
            return View(tWEET);
        }

        // POST: TWEETs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TWEET tWEET = db.TWEETs.Find(id);
            db.TWEETs.Remove(tWEET);
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
