using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Core.Models;
using UserManagement.Services.Interfaces;

namespace UserManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            return Ok(await _permissionService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermission(int id)
        {
            var permission = await _permissionService.GetByIdAsync(id);
            if (permission == null)
                return NotFound();
            return Ok(permission);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission([FromBody] Permission permission)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _permissionService.InsertAsync(permission);
            return CreatedAtAction(nameof(GetPermission), new { id = permission.PermissionId }, permission);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermission(int id, [FromBody] Permission permission)
        {
            if (id != permission.PermissionId)
                return BadRequest("ID mismatch");
            await _permissionService.UpdateAsync(permission);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            var permission = await _permissionService.GetByIdAsync(id);
            if (permission == null)
                return NotFound();
            await _permissionService.DeleteAsync(permission);
            return NoContent();
        }
    }
}
