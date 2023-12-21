using AutoMapper;
using BookService.Dtos;
using BookService.Models;

namespace BookService.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile() 
        {
            // Source -> Target
            CreateMap<Book, BookReadDto>();
            CreateMap<BookCreateDto, Book>(); 
            CreateMap<Book, BookUpdateDto>();
        }
    }
}