using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class TasksController: ControllerBase
    {
        public ApplicationContext context { get; set; }

        public TasksController(ApplicationContext context)
        {
            this.context = context;
        }

        [HttpGet("UserMarks/{userId}")]
        public async Task<IActionResult> GetMarksForUser(int userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(x=>x.Id == userId);
            List<CustomModel3> list = new List<CustomModel3>();
            if (user == null)
            {
                return NotFound();
            }

            if(user.userType == 2)
            {
                list  = await context.CustomModels3.FromSqlInterpolated($"declare @tutorId int; SET @tutorId = {user.Id}; SELECT(users.firstName + ' ' + users.lastName) AS StudentName, users.login AS StudentsLogin, groups.Name AS groupName, disciplines.Name as DisciplineName, tasks.Name AS taskName, studentsTasks.Mark AS Mark, studentsTasks.DatePassed as DatePassed FROM Users users JOIN Groups_Students groupsStudents on groupsStudents.StudentId = users.Id JOIN Groups groups on groups.Id = groupsStudents.GroupId JOIN Student_Tasks studentsTasks on studentsTasks.StudentId = users.Id JOIN Disciplines disciplines on disciplines.DisciplineId = groups.DisciplineId JOIN Tasks  tasks on tasks.Id = studentsTasks.TasksId WHERE groups.TutorId = @tutorId").ToListAsync();
            }
            else if(user.userType == 3)
            {
                list = await context.CustomModels3.FromSqlInterpolated($"SELECT  (users.firstName + ' ' + users.lastName) AS StudentName,users.login AS StudentsLogin,groups.Name AS groupName,tasks.Name as taskName,studentsTasks.DatePassed,studentsTasks.Mark,disciplines.Name AS DisciplineName FROM Users users JOIN Student_Tasks studentsTasks on studentsTasks.StudentId = users.Id JOIN Tasks tasks on tasks.Id = studentsTasks.TasksId  JOIN Groups_Students groupsStudents on groupsStudents.StudentId = users.Id JOIN Groups groups on groups.Id = groupsStudents.GroupId JOIN Disciplines disciplines on disciplines.DisciplineId = tasks.DisciplineId where users.Id = {user.Id}").ToListAsync();
            }

            return Ok(list);
        }

        [HttpPost("Tasks")]
        public async Task<IActionResult> AddTask(Tasks Task)
        {
            var res = await context.Tasks.AddAsync(Task);
            return Ok(res);
        }

        public async Task<IActionResult> GetTasks()
        {
            var res = await context.Tasks.ToListAsync();
            return Ok(res);
        }
    }
}
