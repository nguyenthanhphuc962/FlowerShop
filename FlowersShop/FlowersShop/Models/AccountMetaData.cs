using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlowersShop.Models
{
    public class AccountMetaData
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Address { get; set; }
    }

    [MetadataType(typeof(AccountMetaData))]
    public partial class Account
    {
        
    }
}