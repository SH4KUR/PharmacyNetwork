using System.Collections.Generic;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class PharmacyWharehouse
    {
        public PharmacyWharehouse()
        {
            ReservedMedItem = new HashSet<ReservedMedItem>();
        }

        public int PharmId { get; set; }
        public int MedItemId { get; set; }
        public int ItemCount { get; set; }

        public virtual MedicalItem MedItem { get; set; }
        public virtual Pharmacy Pharm { get; set; }
        public virtual ICollection<ReservedMedItem> ReservedMedItem { get; set; }
    }
}
