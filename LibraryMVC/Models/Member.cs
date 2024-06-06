using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMVC.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 4, ErrorMessage = "The Name field must be between 4 and 80 character")]
        [DisplayName("Members's Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Date of birth")]
        public DateTime? Birth { get; set; }

        [Required]
        public String Gender { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [Phone]
        public String Phone { get; set; }

        [Required]
        [DisplayName("Registration Date")]
        public DateTime? RegistrationDate { get; set; }

        [Required]
        [DisplayName("Expire Date")]
        public DateTime? ExpireDate { get; set; }

        [DisplayName("Member Type")]
        public int MemberTypeId { get; set; }

        public decimal Total { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The Address field must be between 4 and 50 character")]
        public string Address { get; set; }

        public int Status { get; set; }
    }
}