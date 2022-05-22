
namespace WebApplication1.Models
{
    public class Discipline
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Group> Groups { get; set; }
    }
}
