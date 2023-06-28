using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementAPI.Domain.DTOs;
using LibraryManagementAPI.Domain.Entities;
using LibraryManagementAPI.Domain.ViewModels;

namespace webapi.Configuration
{
    public class AutoMapperDTOs : Profile
    {
        public AutoMapperDTOs()
        {
            CreateMap<Book, BookDTO>().PreserveReferences().MaxDepth(0);
            CreateMap<Author, AuthorDTO>().PreserveReferences().MaxDepth(0);
            CreateMap<User, UserDTO>().PreserveReferences().MaxDepth(0);
        }
    }

    public class AutoMapperViewModels : Profile
    {
        public AutoMapperViewModels()
        {
            CreateMap<BookViewModel, Book>();
            CreateMap<AuthorViewModel, Author>();
            CreateMap<UserViewModel, User>();

        }
    }
}