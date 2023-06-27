using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Domain.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public IList<AuthorDTO> Authors { get; set; }
    }
}
