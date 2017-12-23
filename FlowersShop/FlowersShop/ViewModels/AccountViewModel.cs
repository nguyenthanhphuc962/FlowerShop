using FlowersShop.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FlowersShop.ViewModels
{
    public class AccountViewModel
    {
        public Account Account { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }
}