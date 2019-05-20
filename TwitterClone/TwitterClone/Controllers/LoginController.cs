using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class LoginController : Controller
    {
       
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(FormCollection col)
        {
            try
            {
                TwitterDBEntities db = new TwitterDBEntities();
                //db.Configuration.ProxyCreationEnabled = false;
                string user = col["username"].ToString();
                TempData["user"] = user.ToString();
                Session["user"] = user.ToString();
                Session["pwd"] = col["password"].ToString();
                var getUser = db.People.Where(s => s.user_id ==user).ToList();
                    if (getUser != null)
                    {
                    var querypwd = getUser.Select(s=>s.password).ToList();
                    var encrytpwd = Helper.DecryptString(querypwd[0].ToString(), col["password"].ToString());
                        if (encrytpwd != null)
                        {
                        return RedirectToAction("Index", "TWEETs");
                        
                        }
                        ViewBag.ErrorMessage = "Invalid User Name or Password";
                        return View();
                    }
                    ViewBag.ErrorMessage = "Invalid User Name or Password";
                    return View();
                
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = " Error!!! contact cms@info.in";
                return View();
            }
        }


    }
}