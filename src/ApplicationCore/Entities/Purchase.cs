using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class Purchase
    {
        public Purchase()
        {
            Check = new HashSet<Check>();
        }

        [Required]
        [Display(Name = "Код покупки")]
        public int PurchId { get; set; }

        [Required]
        [Display(Name = "Аптека")]
        public int PharmId { get; set; }

        [Required]
        [Display(Name = "Дата и время покупки")]
        public DateTime PurchDate { get; set; }

        [Required]
        [Display(Name = "Сумма покупки")]
        public decimal PurchAmount { get; set; }

        [Required]
        [Display(Name = "Процент скидки")]
        public decimal PurchDiscountPercent { get; set; }

        public virtual Pharmacy Pharm { get; set; }
        public virtual ICollection<Check> Check { get; set; }
    }
}
