using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class BorrowIndexViewModel
    {
        public int BorrowId { get; set; }
        public string MemberName { get; set; }
        public string StaffName { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int Status { get; set; }
    }
}