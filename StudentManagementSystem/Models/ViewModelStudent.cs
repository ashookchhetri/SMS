namespace StudentManagementSystem.Models
{
    public class ViewModelStudent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public long phoneno { get; set; }

        public int FacuiltyId { get; set; }

        public string FacuiltyName { get; set; }

        public string courses { get; set; }
        public IFormFile photo { get; set; }


        public string Imagepath { get; set; }
    }
}
