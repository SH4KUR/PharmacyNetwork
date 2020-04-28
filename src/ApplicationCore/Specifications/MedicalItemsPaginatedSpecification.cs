using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.ApplicationCore.Specifications
{
    public sealed class MedicalItemsPaginatedSpecification : BaseSpecification<MedicalItem>
    {
        public MedicalItemsPaginatedSpecification(int skip, int take, int? categId, int? firmId) 
            : base(i => (!categId.HasValue || i.CategId == categId) && (!firmId.HasValue || i.FirmId == firmId))
        {
            ApplyPaging(skip, take);
        }
    }
}
