using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class MedicalItem
    {
        public MedicalItem()
        {
            Check = new HashSet<Check>();
            IncomeDetail = new HashSet<IncomeDetail>();
            PharmacyWharehouse = new HashSet<PharmacyWharehouse>();
        }

        public int MedItemId { get; set; }

        [Required]
        [Display(Name = "Фирма производитель")]
        public int FirmId { get; set; }

        [Required]
        [Display(Name = "Категория товара")]
        public int CategId { get; set; }

        [Required(ErrorMessage = "Введите название товара!")]
        [Display(Name = "Название товара")]
        public string MedItemName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string MedItemDescrip { get; set; }

        [Required(ErrorMessage = "Введите закупочную цену!")]
        [Display(Name = "Цена (закупочная)")]
        public decimal MedItemPrice { get; set; }

        [Display(Name = "Цена (с наценкой)")]
        public decimal? MedItemPriceMarkup { get; set; }

        public virtual ProductCategory Categ { get; set; }
        public virtual Firm Firm { get; set; }
        public virtual ICollection<Check> Check { get; set; }
        public virtual ICollection<IncomeDetail> IncomeDetail { get; set; }
        public virtual ICollection<PharmacyWharehouse> PharmacyWharehouse { get; set; }
    }
}
