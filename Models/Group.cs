namespace WebApplication1.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TutorId { get; set; }

        public User Tutor { get; set; }

        public bool Status { get; set; }

        public DateTime ChangeDate { get; set; }

        public int DisciplineId { get; set; }

        public Discipline Discipline { get; set; }

        public IEnumerable<User>  Users { get; set; }

        public IEnumerable<UserGroup> UserGroups { get; set; }


    }
}
