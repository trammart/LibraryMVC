using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class MemberType
    {
        [Key]
        public int TypeId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "The Name field must be between 4 and 30 character")]
        [DisplayName("Type's Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Max Books")]
        public int MaxBooks { get; set; }

        [Required]
        [DisplayName("Max Days")]
        public int MaxDays { get; set; }

        [DisplayName("Fee Per Month")]
        public long Fee { get; set; }

        public string Note { get; set; }
    }
}