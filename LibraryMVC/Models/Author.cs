using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryMVC.Models
{
    public class Author : DbContext
    {
        // Your context has been configured to use a 'Author' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LibraryMVC.Models.Author' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Author' 
        // connection string in the application configuration file.
        public Author()
            : base("name=Author")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Authors> Authors { get; set; }
    }

    public class Authors
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 4, ErrorMessage = "The Name field must be between 4 and 30 character")]
        //[RegularExpression("^([A-Z][a-z]+([ ]?[a-z]?['-]?[A-Z][a-z]+)*)$", ErrorMessage = "The author's name is not suitable")]
        [DisplayName("Author's Name")]
        public string Name { get; set; }
    }
}