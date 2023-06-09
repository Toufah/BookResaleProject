namespace BookResale.Models.Dtos
{
    public class SellerBankAccountInfoDto
    {
        public int Id { get; set; }
        public int sellerId { get; set; }
        public string? RIB { get; set; }
        public string? AccountHolderName { get; set; }
    }
}
