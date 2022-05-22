using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<Student_Tasks> Student_Tasks { get; set; }

        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<TaskType> TaskTypes { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<UserType> UserTypes { get; set; }

        public DbSet<CustomModel1> CustomModel1s { get; set; }

        public DbSet<CustomModel2> CustomModels2 { get; set; }

        public DbSet<CustomModel3> CustomModels3 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomModel1>().HasNoKey();
            modelBuilder.Entity<CustomModel2>().HasNoKey();
            modelBuilder.Entity<CustomModel3>().HasNoKey();

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<UserType>().ToTable("userTypes");
            modelBuilder.Entity<UserGroup>().ToTable("Groups_Students");

            modelBuilder.Entity<TaskType>().ToTable("TasksType");
            
            modelBuilder.Entity<Tasks>().ToTable("Tasks");
            modelBuilder.Entity<Student_Tasks>().ToTable("Student_Tasks");
            modelBuilder.Entity<Discipline>().Property(x=>x.Id).HasColumnName("DisciplineId");
            modelBuilder.Entity<UserGroup>().Property(x => x.UserId).HasColumnName("StudentId");
            modelBuilder.Entity<Group>().HasOne(x => x.Tutor).WithMany().HasForeignKey(x => x.TutorId);

            modelBuilder.Entity<Group>().HasOne(x => x.Discipline).WithMany(x => x.Groups).HasForeignKey(x => x.DisciplineId);

            modelBuilder.Entity<Group>()
            .HasMany(p => p.Users)
            .WithMany(p => p.Groups)
            .UsingEntity<UserGroup>(
                j => j
                    .HasOne(pt => pt.Student)
                    .WithMany(t => t.UserGroups)
                    .HasForeignKey(pt => pt.UserId),
                j => j
                    .HasOne(pt => pt.Group)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(pt => pt.GroupId),
                j =>
                {
                    j.HasKey(t => new { t.UserId, t.GroupId });
                });

            modelBuilder.Entity<Tasks>()
            .HasMany(p => p.Students)
            .WithMany(p => p.Tasks)
            .UsingEntity<Student_Tasks>(
                j => j
                    .HasOne(pt => pt.Student)
                    .WithMany(t => t.Student_Tasks)
                    .HasForeignKey(pt => pt.StudentId),
                j => j
                    .HasOne(pt => pt.Task)
                    .WithMany(p => p.StudentTasks)
                    .HasForeignKey(pt => pt.TaskId),
                j =>
                {
                    j.HasKey(t => new { t.TaskId, t.StudentId });
                });


            modelBuilder.Entity<User>().HasOne(x => x.Type).WithMany().HasForeignKey(x=>x.userType);

            

            modelBuilder.Entity<Tasks>().HasOne(x => x.Discipline).WithMany();
        }
    }
}
