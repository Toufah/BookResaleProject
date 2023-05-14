using BookResale.Api.Entities;
using BookResale.Models.Dtos;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace BookResale.Api.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<BookDto> ConvertToDto(this IEnumerable<Book> books,
                                                        IEnumerable<BookCategory> bookcategories,
                                                        IEnumerable<Author> authors,
                                                        IEnumerable<BookState> states)
        {
            return (from book in books
                    join bookCategory in bookcategories on book.CategoryId equals bookCategory.Id
                    join author in authors on book.AuthorId equals author.Id
                    join bookstate in states on book.StateId equals bookstate.Id
                    select new BookDto
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Description,
                        AuthorId = book.AuthorId,
                        AuthorFirstName = author.FirstName,
                        AuthorLastName = author.LastName,
                        ImageURL = book.ImageURL,
                        CategoryId = book.CategoryId,
                        CategoryName = bookCategory.CategoryName,
                        StateId = book.StateId,
                        State = bookstate.State,
                        Price = book.Price,
                        Qty = book.Qty

                    }).ToList();
        }

        public static BookDto ConvertToDto(this Book book,
                                                        BookCategory bookcategorie,
                                                        Author bookAuthor,
                                                        BookState bookState)
        {
            return new BookDto
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Description,
                        AuthorId = book.AuthorId,
                        AuthorFirstName = bookAuthor.FirstName,
                        AuthorLastName = bookAuthor.LastName,
                        ImageURL = book.ImageURL,
                        CategoryId = book.CategoryId,
                        CategoryName = bookcategorie.CategoryName,
                        StateId = book.StateId,
                        State = bookState.State,
                        Price = book.Price,
                        Qty = book.Qty
                    };
        }
    }
}
