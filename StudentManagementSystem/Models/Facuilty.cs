using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Facuilty
    {
        [Key]
        public int Id { get; set; }
        public string FacuiltyName { get; set; }
        
        public ICollection<Courses> courses { get; set; }//navigating properety
        public ICollection<Student> students { get; set; }//navigation property, child table ko parents table ma extract garna lai 



    }
}
