using System;
using System.Collections.Generic;
using System.Text;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.ApplicationCore.Specifications
{
    public class MedItemsInAllPharmSpecification : BaseSpecification<PharmacyWharehouse>
    {
        public MedItemsInAllPharmSpecification(int? medItemId)
            : base(i => (!medItemId.HasValue || i.MedItemId == medItemId))
        {
        }
    }
}
