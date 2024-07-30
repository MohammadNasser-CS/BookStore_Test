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
                Description=book.Description,
                Price=book.Price,
            };
        }
        public static Book CreateBookDto(this CreateBookRequestDto createBookRequest, int CategoryId)
        {
            return new Book
            {
                Title = createBookRequest.Title,
                Description = createBookRequest.Description,
                Price = createBookRequest.Price,
                CatId = CategoryId,
            };
        }

        public static Book UpdateBookDto(this CreateBookRequestDto createBookRequest, int CategoryId)
        {
            return new Book
            {
                Title = createBookRequest.Title,
                Description = createBookRequest.Description,
                Price = createBookRequest.Price,
                CatId = CategoryId,
            };
        }
    }
}