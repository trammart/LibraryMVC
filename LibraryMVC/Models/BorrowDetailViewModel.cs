using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class BorrowingDetailViewModel
    {
        public Borrowing Borrowing { get; set; }
        public List<BorrowDetail> BorrowDetails { get; set; }
    }

}