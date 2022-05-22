namespace WebApplication1.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public UserType Type { get; set; }

        public int userType { get; set; }

        public IEnumerable<Group> Groups { get; set; }

        public IEnumerable<UserGroup> UserGroups { get; set; }

        public IEnumerable<Tasks> Tasks { get; set; }

        public IEnumerable<Student_Tasks> Student_Tasks { get; set; }

    }
}
