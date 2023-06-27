using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Domain.Entities
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public ICollection<Author> Author { get; set; }
        public ICollection<User> BorrowedByUsers { get; set; }
    }
}