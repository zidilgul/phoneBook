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
            return View(db.peopleInDirectory.Where(x => x.userId == id).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(peopleInDirectory person)
        {
            if (Session["idSS"] != null)
            {
                person.userId = int.Parse(Session["idSS"].ToString());

                if (db.peopleInDirectory.Any(x => x.userId == person.userId && x.phone == person.phone))
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

        public ActionResult Edit(int id)
        {
            var p = db.peopleInDirectory.Where(w => w.id == id).FirstOrDefault();
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(peopleInDirectory person)
        {
            person.userId = int.Parse(Session["idSS"].ToString());
            if (person.fullName != null && person.phone != null)
            {
                if (db.peopleInDirectory.Any(a => a.userId == person.userId && a.phone == person.phone))
                {
                    ViewBag.Notification = "Bu telefon rehberinizde zaten kayıtlı!";
                    return View();
                }

                int i = int.Parse(Session["idSS"].ToString());
                var p = db.peopleInDirectory.Where(x => x.id == person.id && x.userId == i).FirstOrDefault();

                if (p == null)
                {
                    ViewBag.Notification = "Böyle bir kişi mevcut değil.";
                    return View();
                }
                else
                {
                  
                    p.phone = person.phone;
                    p.fullName = person.fullName;
                    db.SaveChanges();
                    return RedirectToAction("ListPeople", "People", new { id = p.userId });
                }
            }
            else
            {
                ViewBag.Notification = "Lütfen boş alanları doldurun!";
                return View();
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var p = db.peopleInDirectory.FirstOrDefault(x => x.id == id);
            db.peopleInDirectory.Remove(p);
            db.SaveChanges();
            return RedirectToAction("ListPeople", "People", new { id = p.userId });
        }

    }
}