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

            return Ok(res);
        }
    }
}
