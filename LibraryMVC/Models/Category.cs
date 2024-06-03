using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryMVC.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 4, ErrorMessage = "The Name field must be between 4 and 30 character")]
        [RegularExpression("^([A-Z][a-z]+([ ]?[a-z]?['-]?[A-Z][a-z]+)*)$", ErrorMessage = "Tên sách không h?p l?")]
        [DisplayName("Category's Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Status")]
        public int Status { get; set; } = 1;

        public string StatusText => Status == 1 ? "Active" : "Inactive";
    }
}