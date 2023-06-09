namespace BookResale.Api.Entities
{
    public class SellerBankAccountInfo
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public string? RIB { get; set; }
        public string? AccountHolderName { get; set; }
        public int ApprovalStatus { get; set; }
    }
}
