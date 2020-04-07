namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class Check
    {
        public int MedItemId { get; set; }
        public int PurchId { get; set; }
        public int ItemCount { get; set; }

        public virtual MedicalItem MedItem { get; set; }
        public virtual Purchase Purch { get; set; }
    }
}
