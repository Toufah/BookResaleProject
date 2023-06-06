using BookResale.Models.Dtos;

namespace BookResale.Api.Services.BookServices
{
    public interface IBookService
    {
        public Task<(bool DoTheBookExists, string Message)> AddNewBook(BookDto newBook);
    }
}
