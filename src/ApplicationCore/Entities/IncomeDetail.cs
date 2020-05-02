using System.ComponentModel.DataAnnotations;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class IncomeDetail
    {
        [Required]
        [Display(Name = "Медицинский товар")]
        public int MedItemId { get; set; }

        [Required]
        [Display(Name = "Номер поступления")]
        public int IncomeId { get; set; }

        [Required]
        [Display(Name = "Количество")]
        public int Count { get; set; }

        [Required]
        [Display(Name = "Сумма")]
        public decimal Price { get; set; }

        public virtual Income Income { get; set; }
        public virtual MedicalItem MedItem { get; set; }
    }
}
