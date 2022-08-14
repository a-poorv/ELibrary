using System;
using System.Collections.Generic;

#nullable disable

namespace E_Library.Models
{
    public partial class Book
    {
        public Book()
        {
            LendRequests = new HashSet<LendRequest>();
        }

        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int NoOfCopies { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public string Category { get; set; }
        public string Imageurl { get; set; }
        public int IssuedBooks { get; set; }
        public bool IsAvailable { get; set; }

        public virtual Author Author { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<LendRequest> LendRequests { get; set; }
    }
}
