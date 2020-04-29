using System;
using System.Collections.Generic;
using System.Text;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.ApplicationCore.Specifications
{
    public class MedicalItemsSpecification : BaseSpecification<MedicalItem>
    {
        public MedicalItemsSpecification(int? categId, int? firmId)
            : base(i => (!categId.HasValue || i.CategId == categId) && (!firmId.HasValue || i.FirmId == firmId))
        {
        }
    }
}
