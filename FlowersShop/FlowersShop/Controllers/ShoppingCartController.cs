using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;
using FlowersShop.ViewModels;

namespace FlowersShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly mydatabaseEntities _mde = new mydatabaseEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buy(int id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item()
                {
                    Product = _mde.Products.Find(id),
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExists(id, cart);
                if (index == -1)
                {
                    cart.Add(new Item()
                    {
                        Product = _mde.Products.Find(id),
                        Quantity = 1
                    });
                }
                else
                {
                    cart[index].Quantity++;
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Delete(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExists(id, cart);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index", "ShoppingCart");
        }

        private int isExists(int id, List<Item> cart)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (id == cart[i].Product.Id)
                {
                    return i;
                }
            }
            return -1;
        }

        public ActionResult CheckOut()
        {

            return View();
        }

        public ActionResult Success(FormCollection fc)
        {
            List<Item> list = (List<Item>)Session["cart"];
            Order order = new Order
            {
                CreationDate = DateTime.Now,
                Status = false,
                Name = fc["cusName"]
            };
            _mde.Orders.Add(order);
            _mde.SaveChanges();

            foreach (var item in list)
            {
                OrdersDetail detail = new OrdersDetail
                {
                    OrdersId = order.Id,
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };
                _mde.OrdersDetails.Add(detail);
                _mde.SaveChanges();
            }
            Session.Remove("cart");
            return View();
        }
    }
}