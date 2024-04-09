using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Core.Models;
using UserManagement.Services.Interfaces;

namespace UserManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupPermissionController : ControllerBase
    {
        private readonly IGroupPermissionService _groupPermissionService;

        public GroupPermissionController(IGroupPermissionService groupPermissionService)
        {
            _groupPermissionService = groupPermissionService;
        }

        // GET: api/GroupPermission
        [HttpGet]
        public async Task<IActionResult> GetAllGroupPermissions()
        {
            var groupPermissions = await _groupPermissionService.GetAllAsync();
            return Ok(groupPermissions);
        }

        // GET: api/GroupPermission/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupPermission(int id)
        {
            var groupPermission = await _groupPermissionService.GetByIdAsync(id);
            if (groupPermission == null)
            {
                return NotFound();
            }
            return Ok(groupPermission);
        }
        // POST: api/GroupPermission
        [HttpPost]
        public async Task<IActionResult> CreateGroupPermission([FromBody] GroupPermission groupPermission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _groupPermissionService.InsertAsync(groupPermission);
            return CreatedAtAction(nameof(GetGroupPermission), new { id = groupPermission.GroupId }, groupPermission);
        }

        // PUT: api/GroupPermission/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroupPermission(int id, [FromBody] GroupPermission groupPermission)
        {
            if (id != groupPermission.GroupId)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _groupPermissionService.UpdateAsync(groupPermission);
            return NoContent();
        }

        // DELETE: api/GroupPermission/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupPermission(int id)
        {
            var groupPermission = await _groupPermissionService.GetByIdAsync(id);
            if (groupPermission == null)
            {
                return NotFound();
            }

            await _groupPermissionService.DeleteAsync(groupPermission);
            return NoContent();
        }
    }
}
