using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Domain.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<BookDTO> Books { get; set; }
    }
}