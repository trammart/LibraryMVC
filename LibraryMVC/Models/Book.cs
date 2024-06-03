using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryMVC.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 4, ErrorMessage = "The Name field must be between 4 to 80")]
        [DisplayName("Title")]
        public string BookTitle { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(1, 1000000, ErrorMessage = "The Price field must be between 1 to 1000000")]
        public int Price { get; set; }

        [Display(Name = "Number of Pages")]
        [Required(ErrorMessage = "Number of pages is required")]
        [Range(10, 1000, ErrorMessage = "The Number of Pages field must be between 10 to 1000")]
        public int Pages { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required]
        [DisplayName("Author")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        [Required]
        [DisplayName("Language")]
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }


        [Required]
        [DisplayName("Publisher")]
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }

        [DisplayName("Publication Year")]
        public int PubYear { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Description")]
        [MaxLength(5000)]
        public string Description { get; set; }


        [DisplayName("Created Date")]
        public DateTime? CreatedDate { get; set; }

        [DisplayName("Updated Date")]
        public DateTime? UpdatedDate { get; set; }

        [Required]
        [DisplayName("Status")]
        public int Status { get; set; } = 1;

        public string StatusText => Status == 1 ? "Active" : "Inactive";

    }
}