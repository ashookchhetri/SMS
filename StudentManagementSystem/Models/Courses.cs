using StudentManagementSystem.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int FacuiltyId { get; set; }

        [ForeignKey("FacuiltyId")]
        public virtual Facuilty facuilty { get; set; }

        public ICollection<StudentCourses> studentcourses { get; set; }

    }
}
