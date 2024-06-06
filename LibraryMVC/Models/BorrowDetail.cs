using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class BorrowDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BorrowId { get; set; }
        [ForeignKey("BorrowId")]
        public Borrowing Borrowing { get; set; }

        [Required]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public int Count { get; set; }

    }
}