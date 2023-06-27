using AutoMapper;
using LibraryManagementAPI.Domain.DTOs;
using LibraryManagementAPI.Domain.Entities;

namespace LibraryManagementAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDTO>();
            CreateMap<Author, AuthorDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
