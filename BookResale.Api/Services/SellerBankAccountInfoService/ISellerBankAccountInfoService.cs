using BookResale.Models.Dtos;

namespace BookResale.Api.Services.SellerBankAccountInfoService
{
    public interface ISellerBankAccountInfoService
    {
        Task<bool> AddBankAccount(SellerBankAccountInfoDto sellerBankAccountInfoDto);
        Task<bool> DoBankAccountExists(int sellerId);
    }
}
