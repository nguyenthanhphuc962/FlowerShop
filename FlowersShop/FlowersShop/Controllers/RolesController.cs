using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;

namespace FlowersShop.Controllers
{
    public class RolesController : Controller
    {
        private readonly mydatabaseEntities _mde = new mydatabaseEntities();

        public ActionResult Index()
        {
            ViewBag.list = _mde.Roles.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Create() => View(new Role());

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _mde.Roles.Add(role);
                _mde.SaveChanges();
                return RedirectToAction("Index", "Roles");
            }
            return View(role);
        }

        [HttpGet]
        public ActionResult Edit(int id) => View(_mde.Roles.Find(id));

        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                _mde.Entry(role).State = EntityState.Modified;
                _mde.SaveChanges();
                return RedirectToAction("Index", "Roles");
            }
            return View(role);
        }

        public ActionResult Delete(int id)
        {
            var role = _mde.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            _mde.Roles.Remove(role);
            _mde.SaveChanges();
            return RedirectToAction("Index", "Roles");
        }
    }
}