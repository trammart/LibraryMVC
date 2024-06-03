using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryMVC.Models
{
    public class Category : DbContext
    {
        // Your context has been configured to use a 'Category' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LibraryMVC.Models.Category' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Category' 
        // connection string in the application configuration file.
        public Category()
            : base("name=Category")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Categories> Categories { get; set; }
    }

    public class Categories
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
    }
}