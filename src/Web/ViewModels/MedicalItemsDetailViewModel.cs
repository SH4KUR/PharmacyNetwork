using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.Web.ViewModels
{
    public class MedicalItemsDetailViewModel
    {
        public MedicalItem MedicalItem { get; set; }

        public IEnumerable<PharmacyWharehouse> InPharmList { get; set; }
    }
}
