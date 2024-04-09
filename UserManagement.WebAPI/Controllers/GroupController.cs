using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Core.Models;
using UserManagement.Services.Interfaces;

namespace UserManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            return Ok(await _groupService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            var group = await _groupService.GetByIdAsync(id);
            if (group == null)
                return NotFound();
            return Ok(group);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] Group group)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _groupService.InsertAsync(group);
            return CreatedAtAction(nameof(GetGroup), new { id = group.GroupId }, group);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] Group group)
        {
            if (id != group.GroupId)
                return BadRequest("ID mismatch");
            await _groupService.UpdateAsync(group);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _groupService.GetByIdAsync(id);
            if (group == null)
                return NotFound();
            await _groupService.DeleteAsync(group);
            return NoContent();
        }
    }
}
