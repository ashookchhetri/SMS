using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace StudentManagementSystem.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Remote(action:"IsEmailInUse",controller:"Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Compare("password",ErrorMessage ="password and conformation doesn't match.")]   
        public string conformpassword { get; set; }
        
    }
}
