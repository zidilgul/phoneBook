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
                return RedirectToAction("Login", "Home");
            }


        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(users user)
        {
            var checkLogin = db.users.Where(x => x.userName.Equals(user.userName) && x.password.Equals(user.password)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["idSS"] = checkLogin.id.ToString();
                Session["usernameSS"] = checkLogin.userName.ToString();
                Session["fullNameSS"] = checkLogin.fullName.ToString();
                return RedirectToAction("ListPeople", "People", new { id = checkLogin.id });
            }
            else
            {
                ViewBag.Notification = "Kullanıcı adı veya şifre hatalı!";
                return View();
            }
        }
    }
}