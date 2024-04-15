using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.Domain
{
    public class FacuiltyViewModel
    {
        [Key]
        public int Id { get; set; }
        public string FacuiltyName { get; set; }

        //navigating properety
        public ICollection<Courses> courses { get; set; }

        public int CoursesCount { get; set; }
    }
}
