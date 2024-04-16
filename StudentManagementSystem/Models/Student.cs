using StudentManagementSystem.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number Required!"),
         RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
         ErrorMessage = "Entered phone format is not valid.")]
        public long phoneno { get; set; }

        
        public int FacuiltyId { get; set; }

        [ForeignKey("FacuiltyId")]
        public virtual Facuilty Facuilty { get; set; }//Refrence 

        
        public string imagepath { get; set; }

        public ICollection<StudentCourses> studentcourses { get; set; }
        
  
        
    }
}
