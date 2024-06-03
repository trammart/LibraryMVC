using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryMVC.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 4, ErrorMessage = "The Name field must be between 4 and 80 character")]
        [DisplayName("Publisher's Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The Location field must be between 4 and 50 character")]
        public string Location { get; set; }
    }
}