using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class BorrowingViewModel
    {
        public string MemberPhone { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberTypeName { get; set; }
        public int MaxBooks { get; set; }
        public int MaxDays { get; set; }
        public List<Book> AvailableBooks { get; set; }
        public List<BookViewModel> Books { get; set; }
        //public DateTime ReleaseDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
    public class BookViewModel
    {
        public int BookId { get; set; }
        public int Count { get; set; }
    }
}