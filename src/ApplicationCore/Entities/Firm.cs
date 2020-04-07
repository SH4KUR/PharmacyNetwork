using System.Collections.Generic;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class Firm
    {
        public Firm()
        {
            MedicalItem = new HashSet<MedicalItem>();
        }

        public int FirmId { get; set; }
        public string FirmName { get; set; }
        public string FirmAddress { get; set; }
        public string FirmContact { get; set; }
        public decimal FirmMarkup { get; set; }

        public virtual ICollection<MedicalItem> MedicalItem { get; set; }
    }
}
