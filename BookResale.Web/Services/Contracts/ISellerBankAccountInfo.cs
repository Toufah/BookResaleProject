using BookResale.Models.Dtos;

namespace BookResale.Web.Services.Contracts
{
    public interface ISellerBankAccountInfo
    {
        Task<bool> AddBankAccount(SellerBankAccountInfoDto sellerBankAccountInfoDto);
        Task<bool> DoSellerExists(int sellerId);
    }
}
