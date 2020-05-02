using System;
using System.Collections.Generic;
using System.Text;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.ApplicationCore.Specifications
{
    public class CheckPurchaseSpecification : BaseSpecification<Check>
    {
        public CheckPurchaseSpecification(int? purchaseId)
            : base(i => (!purchaseId.HasValue || i.PurchId == purchaseId))
        {
        }
    }
}