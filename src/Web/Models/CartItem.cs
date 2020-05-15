namespace PharmacyNetwork.Web.Models
{
    public class CartItem
    {
        public int MedicalItemId { get; set; }

        public decimal MedItemPrice { get; set; }

        public int PharmacyId { get; set; }

        public int Count { get; set; }
    }
}
