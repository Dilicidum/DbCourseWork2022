using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/Disciplines")]
    public class DisciplinesController:ControllerBase
    {
        private readonly ApplicationContext context;
        public DisciplinesController(ApplicationContext applicationContext)
        {
            context = applicationContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Discipline>> GetDisciplinesAsync()
        {
            var res = await context.Disciplines.ToListAsync();
            return res;
        }

        [HttpGet("{disciplineId}")]
        public async Task<IActionResult> GetDisciplinesAsync(int disciplineId)
        {
            var res = await context.Disciplines.Include(x=>x.Groups).ThenInclude(x=>x.Tutor).SingleOrDefaultAsync(x=>x.Id == disciplineId);

            if(res == null)
            {
                return BadRequest("no such object with id");
            }

            return Ok(res);
        }

        [HttpGet("DisciplineWithGroupss")]
        public async Task<IActionResult> GetDisciplineWithGroupssAsync()
        {
            var res = await context.CustomModels2.FromSqlInterpolated($"SELECT disciplines.Name AS DisciplineName,COUNT(disciplines.Name) AS NumberOfGroups FROM Disciplines disciplines JOIN Groups groups on groups.DisciplineId = disciplines.DisciplineId GROUP BY disciplines.Name").ToListAsync();
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddDiscipline(Discipline discipline)
        {
            var res = await context.AddAsync(discipline);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{disciplineId}")]
        public async Task<IActionResult> UpdateDiscipline(int disciplineId,[FromBody] Discipline bodyToUpdate)
        {
            var discipline = await context.Disciplines.SingleOrDefaultAsync(x => x.Id == disciplineId);

            if(discipline == null)
            {
                return BadRequest("no such object with selected Id");
            }

            discipline.Name = bodyToUpdate.Name;

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscipline(int id)
        {
            var discipline = await context.Disciplines.SingleOrDefaultAsync(x=>x.Id == id);

            if(discipline == null)
            {
                return BadRequest("No such object with Id");
            }

            context.Disciplines.Remove(discipline);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
