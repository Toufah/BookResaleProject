using BookResale.Api.Entities;
using BookResale.Models.Dtos;
using BookResale.Web.Pages;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Linq;


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

        public static IEnumerable<InboxDto> ConvertToDto(this IEnumerable<Entities.Inbox> messages, IEnumerable<User> users)
        {
            return (from message in messages
                    join senderUser in users on message.SenderId equals senderUser.Id
                    join recipientUser in users on message.RecepientId equals recipientUser.Id
                    select new InboxDto
                    {
                        Id = message.Id,
                        RecepientId = message.RecepientId,
                        RecepientName = recipientUser.LastName + " " + recipientUser.FirstName,
                        SenderId = message.SenderId,
                        SenderName = senderUser.LastName + " " + senderUser.FirstName,
                        Subject = message.Subject,
                        Content = message.Content,
                        Timestamp = message.Timestamp,
                        ReadStatus = message.ReadStatus,
                    }).ToList();
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
        public static IEnumerable<UserDto> ConvertToDto(this IEnumerable<User> users, List<Role> roles)
        {
            return (from user in users
                    join role in roles on user.RoleId equals role.Id
                    select new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        RoleId = user.RoleId,
                        RoleName = role.role,
                    }).ToList();
        }

        public static IEnumerable<OrderDto> ConvertToDto(this IEnumerable<Order> Orders, IEnumerable<Book> Books, IEnumerable<User> Users, IEnumerable<ApprovalStatus> Approvals)
        {
            var orderDtoList = new List<OrderDto>();

            foreach (var order in Orders)
            {
                var orderDto = new OrderDto
                {
                    OrderId = order.OrderId,
                    BooksId = order.BooksId,
                    UserId = order.UserId,
                    UserFirstName = Users.FirstOrDefault(u => u.Id == order.UserId)?.FirstName,
                    UserLastName = Users.FirstOrDefault(u => u.Id == order.UserId)?.LastName,
                    ItemsCount = CalculateItemsCount(order.BooksId),
                    TotalPrice = order.TotalPrice,
                    OrderDate = order.OrderDate,
                    Method = order.Method,
                    Address = order.Address,
                    city = order.city,
                    phoneNumber = order.phoneNumber,
                    ApprovalStatus = Approvals.FirstOrDefault(a => a.id == order.ApprovalStatus).id,
                    ApprovalStatusTitle = Approvals.FirstOrDefault(a => a.id == order.ApprovalStatus).approvalStatusTitle != null ? Approvals.FirstOrDefault(a => a.id == order.ApprovalStatus).approvalStatusTitle : string.Empty,

            };

                orderDtoList.Add(orderDto);
            }

            return orderDtoList;
        }

        private static int CalculateItemsCount(string? booksId)
        {
            if (string.IsNullOrEmpty(booksId))
            {
                return 0;
            }

            string[] bookIds = booksId.Split('/');
            return bookIds.Length;
        }
    }
}
