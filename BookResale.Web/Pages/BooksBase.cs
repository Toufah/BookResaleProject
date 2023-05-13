﻿using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices;

namespace BookResale.Web.Pages
{
    public class BooksBase:ComponentBase
    {
        [Inject]
        public IBookService BookService { get; set; }

        public IEnumerable<BookDto> Books { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Books = await BookService.GetBooks();
        }
    }
}