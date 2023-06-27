using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }
    }
}