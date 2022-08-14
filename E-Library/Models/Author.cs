using System;
using System.Collections.Generic;

#nullable disable

namespace E_Library.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
