using FlowersShop.Models;
using FlowersShop.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FlowersShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly mydatabaseEntities _mde = new mydatabaseEntities();

        public ActionResult Index() => View(_mde.Products.ToList());

        [HttpGet]
        public ActionResult Create()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Product = new Product
            {
                Photo = "thumb1.gif"
            };
            productViewModel.Categories = _mde.Categories.AsEnumerable().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel productViewModel, HttpPostedFileBase photo)
        {
            if (photo != null)
            {
                photo.SaveAs(Server.MapPath("~/Content/imgs/" + photo.FileName));
                productViewModel.Product.Photo = photo.FileName;
            }
            if (ModelState.IsValid)
            {
                _mde.Products.Add(productViewModel.Product);
                _mde.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
            productViewModel.Categories = _mde.Categories.AsEnumerable().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToList();
            return View(productViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Product = _mde.Products.Find(id);
            productViewModel.Categories = _mde.Categories.AsEnumerable().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToList();
            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel productViewModel, HttpPostedFileBase photo)
        {
            if (photo != null)
            {
                photo.SaveAs(Server.MapPath("~/Content/imgs/" + photo.FileName));
                productViewModel.Product.Photo = photo.FileName;
            }
            if (ModelState.IsValid)
            {
                _mde.Entry(productViewModel.Product).State = EntityState.Modified;
                _mde.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
            productViewModel.Categories = _mde.Categories.AsEnumerable().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToList();
            return View(productViewModel);
        }

        public ActionResult Delete(int id)
        {
            var product = _mde.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            _mde.Products.Remove(product);
            _mde.SaveChanges();
            return RedirectToAction("Index", "Products");
        }

        public ActionResult Details(int id)
        {
            Product product = _mde.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.status = product.Status == true ? "Stocking" : "Out of stock";
            ViewBag.product = product;
            return View();
        }
    }
}