using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryMVC.Models
{
    public class Book : DbContext
    {
        // Your context has been configured to use a 'Book' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LibraryMVC.Models.Book' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Book' 
        // connection string in the application configuration file.
        public Book()
            : base("name=Book")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Books> Books { get; set; }
    }

    public class Books
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

        [Required]
        [DisplayName("Author")]
        public int AuthorId { get; set; }

        [Required]
        [DisplayName("Language")]
        public int LanguageId { get; set; }

        [Required]
        [DisplayName("Publisher")]
        public int PublisherId { get; set; }

        [DisplayName("Publication Year")]
        public int PubYear { get; set; }

        [Display(Name = "Description")]
        [MaxLength(5000)]
        public string Description { get; set; }


        [Required]
        [DisplayName("Created Date")]
        public DateTime? CreatedDate { get; set; }

        [Required]
        [DisplayName("Updated Date")]
        public DateTime? UpdatedDate { get; set; }

        [Required]
        [DisplayName("Status")]
        public int Status { get; set; } = 1;

    }
}