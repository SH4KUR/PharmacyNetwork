using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class Income
    {
        public Income()
        {
            IncomeDetail = new HashSet<IncomeDetail>();
        }

        public int IncomeId { get; set; }

        [Required]
        [Display(Name = "Аптека")]
        public int PharmId { get; set; }

        [Required]
        [Display(Name = "Дата и время поступления")]
        public DateTime IncomeDate { get; set; }

        public virtual Pharmacy Pharm { get; set; }
        public virtual ICollection<IncomeDetail> IncomeDetail { get; set; }
    }
}
