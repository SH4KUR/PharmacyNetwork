using System.Collections.Generic;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class Pharmacy
    {
        public Pharmacy()
        {
            Income = new HashSet<Income>();
            PharmacyWharehouse = new HashSet<PharmacyWharehouse>();
            Purchase = new HashSet<Purchase>();
        }

        public int PharmId { get; set; }
        public string PharmName { get; set; }
        public string PharmAddress { get; set; }

        public virtual ICollection<Income> Income { get; set; }
        public virtual ICollection<PharmacyWharehouse> PharmacyWharehouse { get; set; }
        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}
