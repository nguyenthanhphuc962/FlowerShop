using FlowersShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.ViewModels;

namespace FlowersShop.Controllers
{
    public class AccountsController : Controller
    {
        private readonly mydatabaseEntities _mde = new mydatabaseEntities();

        public ActionResult Index()=> View(_mde.Accounts.ToList());

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    AccountViewModel accountViewModel = new AccountViewModel();
        //    accountViewModel.Account = new Account();
        //    accountViewModel.Roles = _mde.AsEnumerable().Roles.Select(r => new SelectListItem
        //    {
        //        Text = r.Name,
        //        Value = r.Id.ToString()
        //    }).ToList();
        //    return View(accountViewModel);
        //}

        [HttpGet]
        public ActionResult Create() => View(new AccountViewModel
        {
            Account = new Account(),
            Roles = _mde.Roles.AsEnumerable().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToList()
        });

        [HttpPost]
        public ActionResult Create(AccountViewModel accountViewModel)
        {
            if (ModelState.IsValid)
            {
                _mde.Accounts.Add(accountViewModel.Account);
                _mde.SaveChanges();
                return RedirectToAction("Index", "Accounts");
            }
            accountViewModel.Roles = _mde.Roles.AsEnumerable().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(string username)
        {
            AccountViewModel accountViewModel = new AccountViewModel();
            accountViewModel.Account = _mde.Accounts.Find(username);
            accountViewModel.Roles = _mde.Roles.AsEnumerable().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToList();
            return View(accountViewModel);
        }

        [HttpPost]
        public ActionResult Edit(AccountViewModel accountViewModel)
        {
            if (ModelState.IsValid)
            {
                _mde.Entry(accountViewModel.Account).State = EntityState.Modified;
                _mde.SaveChanges();
                return RedirectToAction("Index", "Accounts");
            }
            accountViewModel.Roles = _mde.Roles.AsEnumerable().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToList();
            return View(accountViewModel);
        }

        [HttpGet]
        public ActionResult ChangePassword(string username) => View(_mde.Accounts.Find(username));

        public ActionResult Delete(string username)
        {
            var account = _mde.Accounts.Find(username);
            if (account == null)
            {
                return HttpNotFound();
            }
            _mde.Accounts.Remove(account);
            _mde.SaveChanges();
            return RedirectToAction("Index", "Accounts");
        }

        public Account Find(string username) => _mde.Accounts.SingleOrDefault(x => x.Username == username);
        
    }
}