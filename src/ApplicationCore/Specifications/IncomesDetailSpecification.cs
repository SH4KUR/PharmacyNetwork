using System;
using System.Collections.Generic;
using System.Text;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.ApplicationCore.Specifications
{
    public class IncomesDetailSpecification : BaseSpecification<IncomeDetail>
    {
        public IncomesDetailSpecification(int? incomeId)
            : base(i => (!incomeId.HasValue || i.IncomeId == incomeId))
        {
        }
    }
}
