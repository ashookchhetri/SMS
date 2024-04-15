using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class Relationship
    {
        [Key]
        public int CourseId { get; set; }

        [Key]
        public int TeacherId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Courses Courses { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Teachers Teachers { get; set; }
    }
}
