using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext context;
        public UsersController(ApplicationContext applicationContext)
        {
           context = applicationContext;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUsersById(int Id)
        {
            var res = await context.Users.Include(x=>x.Groups).ThenInclude(x=>x.Tutor).FirstOrDefaultAsync(x=>x.Id == Id);
            if(res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(User user)
        {
            if (user == null)
            {
                return BadRequest("Body is required");
            }

            var res = await context.AddAsync(user);
            await context.SaveChangesAsync();

            return Ok("User created");
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(int userId, User user)
        {
            var userToUpdate = await context.Users.SingleOrDefaultAsync(x => x.Id == userId);

            if(userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Login = user.Login;

            //context.Users.Update(userToUpdate);
            await context.SaveChangesAsync();

            return Ok("User updated");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var entity = await context.Users.FirstOrDefaultAsync(x=>x.Id == Id);
            if(entity != null)
            {
                var res = context.Users.Remove(entity);
                await context.SaveChangesAsync();
                return Ok("User deleted");
            }
            return NotFound();
        }

        [HttpGet("UsersWithTypes")]
        public async Task<IActionResult> GetUsersWithTypes(List<int> userTypes)
        {
            if (!userTypes.Any())
            {
                userTypes.AddRange(new int[] {1,2,3});
            }
            var res = await context.Users.Where(x => userTypes.Contains(x.userType)).ToListAsync();
            return Ok(res);
        }

        [HttpGet("GroupsWithTutors")]
        public async Task<IActionResult> GetGroupsWithTutors()
        {
            var res = context.Users
                .Join(context.Groups, x => x.Id, g => g.TutorId, (x, g) => new { u = x, g })
                .Join(context.Disciplines, x=> x.g.DisciplineId, d=>d.Id, (x,d) => 
                    new {GroupName = x.g.Name, DisciplineName = d.Name, TutorFullName = x.u.FirstName + ' ' + x.u.LastName });

            return Ok(res);
        }

        [HttpGet("GroupsWithStudentsAndDisciplines")]
        public async Task<IActionResult> GetGroupsWithStudentsAndDisciplines()
        {
            var e = context.CustomModel1s.FromSqlInterpolated($"SELECT groups.Name AS GroupName,disciplines.Name AS DisciplineName,COUNT(groups.Name) AS AmountOfStudents FROM Users JOIN userTypes userTypes on userTypes.Id = users.userType JOIN Groups_Students groupsStudents on groupsStudents.StudentId = users.Id JOIN Groups groups on groups.Id = groupsStudents.GroupId JOIN Disciplines disciplines on disciplines.DisciplineId = groups.DisciplineId where userType = 3 GROUP BY groups.Name, disciplines.Name");
                

            return Ok(e);
        }

        
                
    }
}
