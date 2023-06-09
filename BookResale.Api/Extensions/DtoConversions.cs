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
                                                        IEnumerable<BookState> states,
                                                        IEnumerable<ApprovalStatus> approvals,
                                                        IEnumerable<User> users)
        {
            return (from book in books
                    join bookCategory in bookcategories on book.CategoryId equals bookCategory.Id
                    join author in authors on book.AuthorId equals author.Id
                    join bookstate in states on book.StateId equals bookstate.Id
                    join approvalStatus in approvals on book.approvalStatus equals approvalStatus.id
                    join user in users on book.sellerId equals user.Id
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
                        Qty = book.Qty,
                        approvalStatus = book.approvalStatus,
                        approvalStatusTitle = approvalStatus.approvalStatusTitle,
                        sellerId = book.sellerId,
                        sellerFirstname = user.FirstName,
                        sellerLastname = user.LastName

                    }).ToList();
        }

        public static BookDto ConvertToDto(this Book book,
                                                        BookCategory bookcategorie,
                                                        Author bookAuthor,
                                                        BookState bookState,
                                                        ApprovalStatus approval,
                                                        User user)
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
                        Qty = book.Qty,
                        approvalStatus = book.approvalStatus,
                        approvalStatusTitle = approval.approvalStatusTitle,
                        sellerId = book.sellerId,
                        sellerFirstname = user.FirstName,
                        sellerLastname = user.LastName
            };
        }


        public static List<CartItemDto> ConvertToDto(this List<Book> books)
        {
            var cartItems = new List<CartItemDto>();
            foreach (var book in books)
            {
                var cartItem = new CartItemDto
                {
                    BookId = book.Id,
                    BookTitle = book.Title,
                    BookImageURL = book.ImageURL,
                    Price = book.Price,
                    Qty = book.Qty
                };
                cartItems.Add(cartItem);
            }
            return cartItems;
        }

        public static List<CategoryDto> ConvertToDto(this IEnumerable<BookCategory> categories)
        {
            return categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            }).ToList();
        }

        public static List<StateDto> ConvertToDto(this IEnumerable<BookState> states)
        {
            return states.Select(state => new StateDto
            {
                Id = state.Id,
                State = state.State
            }).ToList();
        }

        public static List<AuthorDto> ConvertToDto(this IEnumerable<Author> authors)
        {
            return authors.Select(author => new AuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Birthplace = author.Birthplace,
                Born = author.Born,
                Died = author.Died,
                ImageURL = author.ImageURL
            }).ToList();
        }

        public static AuthorDto ConvertToDto(this Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
            };
        }

        public static CategoryDto ConvertToDto(this BookCategory category)
        {
            return new CategoryDto
            {
                Id=category.Id,
                CategoryName=category.CategoryName,
            };
        }

        public static StateDto ConvertToDto(this BookState state)
        {
            return new StateDto
            {
                Id = state.Id,
                State = state.State,
            };
        }
    }
}
