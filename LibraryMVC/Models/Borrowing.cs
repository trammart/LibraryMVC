using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace LibraryMVC.Models
{
    public class Borrowing
    {
        [Key]
        public int BorrowId { get; set; }

        [Required]
        public int MemberId { get; set;}

        [Required]
        public string StaffId { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        public DateTime? DueDate { get; set; }

        [Required]
        public int Status { get; set; }
    }
}