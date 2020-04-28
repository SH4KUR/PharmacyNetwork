using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.Web.ViewModels
{
    public class MedicalItemsViewModel
    {
        public IEnumerable<MedicalItem> MedicalItems { get; set; }

        public PaginationViewModel Pagination { get; set; }
    }
}
