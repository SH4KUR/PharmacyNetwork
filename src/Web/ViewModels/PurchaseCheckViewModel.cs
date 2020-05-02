using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.Web.ViewModels
{
    public class PurchaseCheckViewModel
    {
        public Purchase Purchase { get; set; }

        public IEnumerable<Check> Check { get; set; }
    }
}
