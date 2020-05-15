using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;

namespace PharmacyNetwork.Web.ViewModels
{
    public class CreateIncomeViewModel
    {
        public List<IncomeItem> ListIncomes { get; set; }

        public int? PharmacyId { get; set; }

        public IEnumerable<SelectListItem> Pharmacies { get; set; }

        public IEnumerable<MedicalItem> MedicalItems { get; set; }
    }

    public class IncomeItem
    {
        public int MedicalItemId { get; set; }

        public int Count { get; set; }
    }
}
