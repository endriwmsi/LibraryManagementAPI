using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementAPI.Data.Context;
using LibraryManagementAPI.Domain.Entities;
using LibraryManagementAPI.Domain.Interfaces;

namespace LibraryManagementAPI.Data.Repositories
{
    public class AuthorBookRepository : IAuthorBookRepository
    {
        private readonly DataContext _context;

        public AuthorBookRepository(DataContext context)
        {
            _context = context;
        }

        public void AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }
}