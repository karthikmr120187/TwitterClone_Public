using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TwitterClone;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class SignUpController : Controller
    {
        private TwitterDBEntities db = new TwitterDBEntities();

        // GET: SignUp
        //public ActionResult Index()
        //{
        //    return View(db.People.ToList());
        //}

        // GET: SignUp/Details/5
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

        // GET: SignUp/Create
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: SignUp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                PERSON person=new PERSON();
                person.user_id=collection["txtUsername"].ToString();
                person.fullName = collection["txtFullName"].ToString();
                //person.password = collection["txtPwd"].ToString();
                person.password = Convert.ToString(Helper.EncryptString(collection["txtPwd"].ToString(), collection["txtPwd"].ToString()));
                person.email = collection["txtemail"].ToString();
                person.joined = DateTime.Now;
                person.active = true;
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Login","Login");
            }

            return View();
        }

        // GET: SignUp/Edit/5
        public ActionResult Edit(string id)
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

        // POST: SignUp/Edit/5
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

        // GET: SignUp/Delete/5
        public ActionResult Delete(string id)
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

        // POST: SignUp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PERSON pERSON = db.People.Find(id);
            db.People.Remove(pERSON);
            db.SaveChanges();
            return RedirectToAction("Login");
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
