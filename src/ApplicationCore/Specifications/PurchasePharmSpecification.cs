using System;
using System.Collections.Generic;
using System.Text;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.ApplicationCore.Specifications
{
    public class PurchasePharmSpecification : BaseSpecification<Purchase>
    {
        public PurchasePharmSpecification(int? pharmId)
            : base(i => (!pharmId.HasValue || i.PharmId == pharmId))
        {
            ApplyOrderByDescending(i => i.PurchId);
        }
    }
}
