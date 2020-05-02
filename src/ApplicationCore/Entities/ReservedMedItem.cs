using System;
using System.ComponentModel.DataAnnotations;

namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class ReservedMedItem
    {
        [Required]
        [Display(Name = "Номер резервации")]
        public int ReservedId { get; set; }

        [Required]
        [Display(Name = "Дата и время начала резервации")]
        public DateTime DateStart { get; set; }

        [Required]
        [Display(Name = "Дата и время окончания резервации")]
        public DateTime DateFinish { get; set; }

        [Required]
        [Display(Name = "Медицинский товар")]
        public int MedItemId { get; set; }

        [Required]
        [Display(Name = "Аптека")]
        public int PharmId { get; set; }

        [Required]
        [Display(Name = "Количество")]
        public int Count { get; set; }

        [Required]
        [Display(Name = "Номер телефона клиента")]
        public string Telephone { get; set; }

        public virtual PharmacyWharehouse PharmacyWharehouse { get; set; }
    }
}
