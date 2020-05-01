using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.Web.ViewModels
{
    public class MedicalItemViewModel
    {
        public MedicalItem MedicalItem { get; set; }

        public IEnumerable<SelectListItem> Firms { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
