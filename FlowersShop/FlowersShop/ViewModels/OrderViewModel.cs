using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlowersShop.Models;

namespace FlowersShop.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<Product> Products { get; set; }
    }
}