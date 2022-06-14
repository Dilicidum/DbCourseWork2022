namespace WebApplication1.Models
{
    public class Student_Tasks
    {
        public int StudentId { get; set; }

        public User Student { get; set; }

        public int TasksId { get; set; }

        public Tasks Task { get; set; }

        public double Mark { get; set; }

        public DateTime DatePassed { get; set; }
    }
}
