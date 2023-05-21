using BookResale.Models.Dtos;

namespace BookResale.Web.Services.Contracts
{
    public interface IAuthorsService
    {
        Task<IEnumerable<AuthorDto>> GetAuthors();
    }
}
