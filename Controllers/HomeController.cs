using phoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace phoneBook.Controllers
{
    public class HomeController : Controller
    {
        RehberEntities db = new RehberEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(users user)
        {
            if (db.users.Any(x => x.userName == user.userName))
            {
                ViewBag.Notification = "Bu kullanıcı adı zaten var";
                return View();
            }
            else
            {
                db.users.Add(user);
                db.SaveChanges();

                Session["idSS"] = user.id.ToString();
                Session["usernameSS"] = user.userName.ToString();
                Session["fullNameSS"] = user.fullName.ToString();
                return RedirectToAction("Index", "Home");
            }


        }
    }
}