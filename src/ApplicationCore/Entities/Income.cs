using System;
using System.Collections.Generic;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class Income
    {
        public Income()
        {
            IncomeDetail = new HashSet<IncomeDetail>();
        }

        public int IncomeId { get; set; }
        public int PharmId { get; set; }
        public DateTime IncomeDate { get; set; }

        public virtual Pharmacy Pharm { get; set; }
        public virtual ICollection<IncomeDetail> IncomeDetail { get; set; }
    }
}
