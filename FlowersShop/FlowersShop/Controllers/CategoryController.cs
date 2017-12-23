using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;

namespace FlowersShop.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly mydatabaseEntities _mde = new mydatabaseEntities();

        public ActionResult Index()
        {
            ViewBag.list = _mde.Categories.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Create() => View(new Category());

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _mde.Categories.Add(category);
                _mde.SaveChanges();
                return RedirectToAction("Index", "Categories");
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult Edit(int id) => View(_mde.Categories.Find(id));

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _mde.Entry(category).State = EntityState.Modified;
                _mde.SaveChanges();
                return RedirectToAction("Index", "Categories");
            }
            return View(category);
        }

        public ActionResult Delete(int id)
        {
            var category = _mde.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            _mde.Categories.Remove(category);
            _mde.SaveChanges();
            return RedirectToAction("Index", "Categories");
        }
    }
}