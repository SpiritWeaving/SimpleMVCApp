namespace MvcApp.Models
{
    public class Department
    {
        public string Head {  get; set; }       
        public int FacultyId { get; set; }
        public string Name { get; set; }
        public string Profile { get; set; }
        public int Professors {  get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string ImagePath { get; set; }

        public Department(int facultyId, string head, string name,
            string profile, int professors, string email, string location,
            string imagePath)
        {
            FacultyId = facultyId;
            Head = head;
            Name = name;
            Profile = profile;
            Professors = professors;
            Email = email;
            Location = location;
            ImagePath = imagePath;
        }

    }
}
