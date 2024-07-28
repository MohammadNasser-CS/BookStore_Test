using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExample.Dtos.Book;
using ApiExample.Models;

namespace ApiExample.Mapper
{
    public static class BookMappers
    {
        public static BookDto ToBookDto(this Book book)
        {
            return new BookDto
            {
                Title = book.Title,
            };
        }
        public static Book CreateBookDto(this CreateBookRequestDto createBookRequest)
        {
            return new Book
            {
                Title = createBookRequest.Title,
            };
        }
    }
}