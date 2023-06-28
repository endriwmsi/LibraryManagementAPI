using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        // public IList<Book> BorrowedBooks { get; set; }
    }
}