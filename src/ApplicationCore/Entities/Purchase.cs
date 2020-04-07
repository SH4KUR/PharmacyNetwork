using System;
using System.Collections.Generic;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class Purchase
    {
        public Purchase()
        {
            Check = new HashSet<Check>();
        }

        public int PurchId { get; set; }
        public int PharmId { get; set; }
        public DateTime PurchDate { get; set; }
        public decimal PurchAmount { get; set; }
        public decimal PurchDiscountPercent { get; set; }

        public virtual Pharmacy Pharm { get; set; }
        public virtual ICollection<Check> Check { get; set; }
    }
}
