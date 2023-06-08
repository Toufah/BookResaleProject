﻿using BookResale.Models.Dtos;
using BookResale.Web.ViewModels;

namespace BookResale.Web.Services.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBooks();
        Task<BookDto> GetBook(long id);
        Task<bool> AddNewBook(BookDto book);
        Task<IEnumerable<BookDto>> GetRecentlyViewedBooks(int userId);
        Task<IEnumerable<BookDto>> GetBooksWithCategory(int categoryId);
        Task<CategoryDto> GetTopViewedCategory(int userId);
    }
}
