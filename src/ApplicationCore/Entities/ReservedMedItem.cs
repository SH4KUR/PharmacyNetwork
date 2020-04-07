using System;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class ReservedMedItem
    {
        public int ReservedId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public int MedItemId { get; set; }
        public int PharmId { get; set; }
        public int Count { get; set; }
        public string Telephone { get; set; }

        public virtual PharmacyWharehouse PharmacyWharehouse { get; set; }
    }
}
