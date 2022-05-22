using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/Groups")]
    public class GroupsController:ControllerBase
    {
        private readonly ApplicationContext context;
        public GroupsController(ApplicationContext applicationContext)
        {
            context = applicationContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            return await context.Groups.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup(Group group)
        {
            var res = await context.Groups.AddAsync(group);
            return Ok(res);
        }
    }
}
