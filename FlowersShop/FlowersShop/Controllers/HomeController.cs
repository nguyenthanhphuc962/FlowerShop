using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;

namespace FlowersShop.Controllers
{
    public class HomeController : Controller
    {

        private readonly mydatabaseEntities _mde = new mydatabaseEntities();

        public ActionResult Index() => View(_mde.Orders.OrderBy(x => x.CreationDate).Take(10).ToList());
    }
}