using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.Web.ViewModels
{
    public class TransferViewModel
    {
        public Pharmacy Pharmacy { get; set; }

        public MedicalItem MedicalItem { get; set; }

        public IEnumerable<SelectListItem> Pharmacies { get; set; }

        public int MaxItemCount { get; set; }

        public int TransferItemCount { get; set; }

        public int TransferPharmId { get; set; }
    }
}
