using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Domain.Entities
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public IList<AuthorBook> Authors { get; set; }
    }
}