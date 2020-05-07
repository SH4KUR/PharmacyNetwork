using System;
using System.Collections.Generic;
using System.Text;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.ApplicationCore.Specifications
{
    public class MedItemsByPharmWharehouseSpecification : BaseSpecification<PharmacyWharehouse>
    {
        public MedItemsByPharmWharehouseSpecification(int? pharmId)
            : base(i => (!pharmId.HasValue || i.PharmId == pharmId))
        {
        }
    }
}
