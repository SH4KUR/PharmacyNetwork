using System.ComponentModel.DataAnnotations;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class Check
    {
        [Required]
        [Display(Name = "Медицинский товар")]
        public int MedItemId { get; set; }

        [Required]
        [Display(Name = "Код покупки")]
        public int PurchId { get; set; }

        [Required]
        [Display(Name = "Количество")]
        public int ItemCount { get; set; }

        public virtual MedicalItem MedItem { get; set; }
        public virtual Purchase Purch { get; set; }
    }
}
