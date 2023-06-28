using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementAPI.Data.Context;
using LibraryManagementAPI.Domain.Entities;
using LibraryManagementAPI.Domain.Interfaces;

namespace LibraryManagementAPI.Domain.Interfaces
{
    public interface IAuthorBookRepository
    {
        void AddAuthor(Author author);
        void AddBook(Book Book);
    }
}