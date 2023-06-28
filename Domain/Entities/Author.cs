using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Domain.Entities
{
    public class Author : Entity
    {   
        public string Name { get; set; }
        public IList<AuthorBook> Books { get; set; }
    }
}