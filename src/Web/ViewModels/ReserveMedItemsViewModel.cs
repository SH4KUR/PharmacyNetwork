using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacyNetwork.Web.Models;

namespace PharmacyNetwork.Web.ViewModels
{
    public class ReserveMedItemsViewModel
    {
        public List<CartItem> Items { get; set; }

        [Required]
        public string Telephone { get; set; }
    }
}
