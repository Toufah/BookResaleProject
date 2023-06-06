using BookResale.Models.Dtos;

namespace BookResale.Web.Services.Contracts
{
    public interface IStateService
    {
        Task<IEnumerable<StateDto>> GetBookStates();
        Task<StateDto> GetState(int id);
    }
}
