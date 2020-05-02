using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class PharmacyWharehouse
    {
        public PharmacyWharehouse()
        {
            ReservedMedItem = new HashSet<ReservedMedItem>();
        }

        [Required]
        [Display(Name = "Аптека")]
        public int PharmId { get; set; }

        [Required]
        [Display(Name = "Медицинский товар")]
        public int MedItemId { get; set; }

        [Required]
        [Display(Name = "Количество на складе")]
        public int ItemCount { get; set; }

        public virtual MedicalItem MedItem { get; set; }
        public virtual Pharmacy Pharm { get; set; }
        public virtual ICollection<ReservedMedItem> ReservedMedItem { get; set; }
    }
}
