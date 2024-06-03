using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryMVC.Models
{
    public class Publisher : DbContext
    {
        // Your context has been configured to use a 'Publisher' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LibraryMVC.Models.Publisher' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Publisher' 
        // connection string in the application configuration file.
        public Publisher()
            : base("name=Publisher")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Publishers> Publishers { get; set; }
    }

    public class Publishers
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