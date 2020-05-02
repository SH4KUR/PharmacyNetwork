using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.Web.ViewModels
{
    public class IncomeDetailViewModel
    {
        public Income Income { get; set; }

        public IEnumerable<IncomeDetail> IncomeDetails { get; set; }
    }
}
