namespace WebApplication1.Models
{
    public class Tasks
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TaskType TaskType { get; set; }

        public int DisciplineId { get; set; }

        public Discipline Discipline { get; set; }

        public IEnumerable<Student_Tasks> StudentTasks { get; set; }

        public IEnumerable<User> Students { get; set; }


    }
}
