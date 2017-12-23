using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;
using FlowersShop.ViewModels;

namespace FlowersShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly mydatabaseEntities _mde = new mydatabaseEntities();

        public ActionResult Index() => View(_mde.Orders.OrderBy(x => x.CreationDate).ToList());

        public ActionResult NewOrders() => View(_mde.Orders.OrderBy(x => x.CreationDate).Take(10).ToList());

        [HttpGet]
        public ActionResult Create()
        {
            List<Item> cart = (List<Item>)Session["cart"];
            Order order = new Order();
            order.CreationDate = DateTime.Now;
            _mde.Orders.Add(order);
            _mde.SaveChanges();
            List<OrdersDetail> ordersDetails = new List<OrdersDetail>();
            foreach (var item in cart)
            {
                OrdersDetail ordersDetail = new OrdersDetail();
                ordersDetail.OrdersId = order.Id;
                ordersDetail.ProductId = item.Product.Id;
                ordersDetail.Quantity = item.Quantity;
                ordersDetail.Price = item.Quantity * item.Product.Price;
                ordersDetails.Add(ordersDetail);
            }
            return View(ordersDetails);
        }
    }
}