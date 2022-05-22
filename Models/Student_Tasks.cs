namespace WebApplication1.Models
{
    public class Student_Tasks
    {
        public int StudentId { get; set; }

        public User Student { get; set; }

        public int TaskId { get; set; }

        public Tasks Task { get; set; }

        public int Mark { get; set; }

        public DateTime DatePassed { get; set; }
    }
}
