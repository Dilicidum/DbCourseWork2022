namespace WebApplication1.Models
{
    public class UserGroup
    {
        public int? UserId { get; set; }

        public int? GroupId { get; set; }

        public User Student { get; set; }

        public Group Group { get; set; }
    }
}
