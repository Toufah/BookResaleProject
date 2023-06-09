using BookResale.Api.Data;
using BookResale.Api.Entities;
using BookResale.Models.Dtos;

namespace BookResale.Api.Services.SellerBankAccountInfoService
{
    public class SellersBankAccountInfoService : ISellerBankAccountInfoService
    {
        private readonly BookResaleDbContext bookResaleDbContext;

        public SellersBankAccountInfoService(BookResaleDbContext bookResaleDbContext)
        {
            this.bookResaleDbContext = bookResaleDbContext;
        }
        public async Task<bool> AddBankAccount(SellerBankAccountInfoDto sellerBankAccountInfoDto)
        {
            try
            {
                var doTheSellerExists = bookResaleDbContext.SellersBankAccountInfo.Any(_ => _.SellerId == sellerBankAccountInfoDto.sellerId);
                if (doTheSellerExists)
                {
                    return false;
                }
                var bankAccountInfo = new SellerBankAccountInfo
                {
                    SellerId = sellerBankAccountInfoDto.sellerId,
                    RIB = sellerBankAccountInfoDto.RIB,
                    AccountHolderName = sellerBankAccountInfoDto.AccountHolderName,
                    ApprovalStatus = 1,
                };
                bookResaleDbContext.Add(bankAccountInfo);
                await bookResaleDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DoBankAccountExists(int sellerId)
        {
            try
            {
                var doSellerExists = bookResaleDbContext.SellersBankAccountInfo.Any(_ => _.SellerId == sellerId);
                if (doSellerExists)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
