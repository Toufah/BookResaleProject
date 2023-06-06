using BookResale.Models.Dtos;

namespace BookResale.Web.Services.Contracts
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task<CategoryDto> GetCategory(int id);
    }
}
