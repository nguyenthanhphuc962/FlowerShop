using FlowersShop.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FlowersShop.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}