using System;
using System.Collections.Generic;
using System.Text;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.ApplicationCore.Specifications
{
    public class ReserveMedItemsByPharmacySpecification : BaseSpecification<ReservedMedItem>
    {
        public ReserveMedItemsByPharmacySpecification(int? pharmId)
            : base(i => (!pharmId.HasValue || i.PharmId == pharmId))
        {
            ApplyOrderByDescending(i => i.ReservedId);
        }
    }
}
