using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.Web.ViewModels
{
    public class PharmacyViewModel
    {
        public Pharmacy Pharmacy { get; set; }

        public List<PharmacyWharehouse> MedicalItems { get; set; }
    }
}
