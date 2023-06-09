using BookResale.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookResale.Api.Data
{
    public class BookResaleDbContext:DbContext
    {
        public BookResaleDbContext(DbContextOptions<BookResaleDbContext> options) : base(options){ }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //books
            modelBuilder.Entity<Book>().HasData(new Book
            {
                Id = 1214689745812,
                Title = "The subtle art of not giving a f*ck",
                AuthorId = 1,
                Description = "A self-help book written by Mark Manson. The book challenges traditional self-help advice by arguing that the key to a fulfilling life is not to try to be happy all the time, but to embrace the inevitable struggles and failures that come with life.",
                ImageURL = "/Images/self-help/TheSubtleArtOfNotGivingAFuck.webp",
                Price = 100,
                StateId = 1,
                Qty = 12,
                CategoryId = 1

            });
            modelBuilder.Entity<Book>().HasData(new Book
            {
                Id = 1687421938751,
                Title = "Rich dad poor dad",
                AuthorId = 2,
                Description = "A personal finance book written by Robert Kiyosaki. The book is structured as a series of lessons that Kiyosaki learned from his RICH DAD and his POOR DAD, who had contrasting attitudes towards money and wealth.",
                ImageURL = "/Images/self-help/RichDadPoorDad.webp",
                Price = 80,
                StateId = 0,
                Qty = 10,
                CategoryId = 0

            });
            modelBuilder.Entity<Book>().HasData(new Book
            {
                Id = 1687421875751,
                Title = "The Richest Man in Babylon",
                AuthorId = 1,
                Description = "A classic personal finance book written by   . The book uses parables set in ancient Babylon to teach timeless lessons about money management and wealth creation.",
                ImageURL = "/Images/self-help/TheRichestManinBabylon.webp",
                Price = 120,
                StateId = 0,
                Qty = 8,
                CategoryId = 2

            });
            modelBuilder.Entity<Book>().HasData(new Book
            {
                Id = 9999999999999,
                Title = "Test",
                AuthorId = 1,
                Description = "Test.",
                ImageURL = "/Images/self-help/TheRichestManinBabylon.webp",
                Price = 120,
                StateId = 0,
                Qty = 8,
                CategoryId = 2

            });
            modelBuilder.Entity<Book>().HasData(new Book
            {
                Id = 9999999999998,
                Title = "Test",
                AuthorId = 1,
                Description = "Test.",
                ImageURL = "/Images/self-help/TheRichestManinBabylon.webp",
                Price = 120,
                StateId = 0,
                Qty = 8,
                CategoryId = 2

            });

            //Add users
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,

                FirstName = "Bob",

                LastName = "jackson",

                CID = "W452876",

                Email = "bob@gmail.com",

                Password = "Bob2001@",

                Phone = "0666603548",

                Role = 0

            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,

                FirstName = "sarah",

                LastName = "hasner",

                CID = "W852876",

                Email = "sarah@gmail.com",

                Password = "sarah2001@",

                Phone = "0766603548",

                Role = 1

            });

            //Create Shopping Cart for Users
            modelBuilder.Entity<Cart>().HasData(new Cart
            {
                Id = 1,
                UserId = 1

            });
            modelBuilder.Entity<Cart>().HasData(new Cart
            {
                Id = 2,
                UserId = 2

            });

            //Add Book Categories
            modelBuilder.Entity<BookCategory>().HasData(new BookCategory
            {
                Id = 1,
                CategoryName = "self-help"
            });
            modelBuilder.Entity<BookCategory>().HasData(new BookCategory
            {
                Id = 2,
                CategoryName = "history "
            });

            //Add Authors
            modelBuilder.Entity<Author>().HasData(new Author
            {
                Id = 1,
                FirstName = "Mark",
                LastName = "Manson",
                Born = "March 9, 1984",
                Died = "",
                Birthplace = "America",
                ImageURL = "/Images/Beauty/MarkManson.png",
            });

            modelBuilder.Entity<Author>().HasData(new Author
            {
                Id = 2,
                FirstName = "Robert",
                LastName = "Kiyosaki",
                Born = "April 8, 1947",
                Died = "",
                Birthplace = "Hilo, Hawaii, U.S",
                ImageURL = "/Images/Beauty/Robert.png",
            });

            modelBuilder.Entity<Author>().HasData(new Author
            {
                Id = 3,
                FirstName = "George",
                LastName = "Samuel Clason",
                Born = "November 7, 1874",
                Died = "April 5, 1957",
                Birthplace = "America",
                ImageURL = "/Images/Beauty/SamuelClason.png",
            });

            //Book States
            modelBuilder.Entity<BookState>().HasData(new BookState
            {
                Id = 1,
                State = "New"
            });
            modelBuilder.Entity<BookState>().HasData(new BookState
            {
                Id = 2,
                State = "Second-hand"
            });
            modelBuilder.Entity<BookState>().HasData(new BookState
            {
                Id = 3,
                State = "Old"
            });
        }*/

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookState> BookStates { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public DbSet<UserActivityLog> UserActivityLog { get; set; }
        public DbSet<UserShippingAddress> UserShippingAddress { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ApprovalStatus> approvalStatus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SellerBankAccountInfo> SellersBankAccountInfo { get; set; }
        public DbSet<Inbox> Inbox { get; set; }
    }
}
