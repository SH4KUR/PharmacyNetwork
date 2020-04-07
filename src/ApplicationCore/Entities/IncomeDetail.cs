namespace PharmacyNetwork.ApplicationCore.Entities
{
    public partial class IncomeDetail
    {
        public int MedItemId { get; set; }
        public int IncomeId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public virtual Income Income { get; set; }
        public virtual MedicalItem MedItem { get; set; }
    }
}
