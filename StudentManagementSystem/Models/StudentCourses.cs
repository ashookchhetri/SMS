using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.Domain
{
    public class StudentCourses
    {
        [Key]
        public int Id { get; set; }

        
        public int? StudentId { get; set; }
        public Student Student { get; set; }

        
        public int? CourseId { get; set; }
        public Courses Courses { get; set; }

   
        
        
        
    

    }
}
