using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlowersShop.Models;

namespace FlowersShop.ViewModels
{
    public class Item
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}