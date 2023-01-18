using phoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace phoneBook.Controllers
{
    public class PeopleController : Controller
    {
        RehberEntities db = new RehberEntities();

        // GET: People

        [HttpGet]
        public ActionResult ListPeople(int id)
        {
            return View(db.peopleInDirectory.Where(x=> x.userId == id).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(peopleInDirectory person)
        {
            if (Session["idSS"] != null) {
                person.userId = int.Parse(Session["idSS"].ToString());

                if (db.peopleInDirectory.Any(x=> x.userId == person.userId && x.phone == person.phone))
            {
                ViewBag.Notification = "Bu telefon rehberinizde zaten kayıtlı!";
                return View();
            }
            else
            {
                db.peopleInDirectory.Add(person);
                db.SaveChanges();
                    return RedirectToAction("ListPeople", "People", new { id = person.userId });
                }
            }
            else
            {
                ViewBag.Notification = "Lütfen giriş yapın!";
                return View();
            }
        }

    }
}