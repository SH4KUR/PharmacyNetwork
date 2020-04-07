using System.Collections.Generic;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            MedicalItem = new HashSet<MedicalItem>();
        }

        public int CategId { get; set; }
        public string CategName { get; set; }
        public decimal CategMarkup { get; set; }

        public virtual ICollection<MedicalItem> MedicalItem { get; set; }
    }
}
