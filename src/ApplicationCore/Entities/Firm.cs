using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class Firm
    {
        public Firm()
        {
            MedicalItem = new HashSet<MedicalItem>();
        }

        public int FirmId { get; set; }

        [Required]
        [Display(Name = "Название фирмы")]
        public string FirmName { get; set; }

        [Required]
        [Display(Name = "Адрес фирмы")]
        public string FirmAddress { get; set; }

        [Required]
        [Display(Name = "Контакты фирмы")]
        public string FirmContact { get; set; }

        [Required]
        [Display(Name = "Процент наценки на фирму")]
        [Range(typeof(decimal), "0,1", "99,99")]
        public decimal FirmMarkup { get; set; }

        public virtual ICollection<MedicalItem> MedicalItem { get; set; }
    }
}
