using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Core.Models;
using UserManagement.Services.Interfaces;

namespace UserManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly IUserGroupService _userGroupService;

        public UserGroupController(IUserGroupService userGroupService)
        {
            _userGroupService = userGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserGroups()
        {
            return Ok(await _userGroupService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserGroup(int id)
        {
            var userGroup = await _userGroupService.GetByIdAsync(id);
            if (userGroup == null)
                return NotFound();
            return Ok(userGroup);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserGroup([FromBody] UserGroup userGroup)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _userGroupService.InsertAsync(userGroup);
            return CreatedAtAction(nameof(GetUserGroup), new { id = userGroup.UserId }, userGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserGroup(int id, [FromBody] UserGroup userGroup)
        {
            if (id != userGroup.UserId)
                return BadRequest("ID mismatch");
            await _userGroupService.UpdateAsync(userGroup);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserGroup(int id)
        {
            var userGroup = await _userGroupService.GetByIdAsync(id);
            if (userGroup == null)
                return NotFound();
            await _userGroupService.DeleteAsync(userGroup);
            return NoContent();
        }
        [HttpGet("UsersPerGroupCount")]
        public async Task<ActionResult<Dictionary<int, int>>> GetUsersPerGroupCount()
        {
            try
            {
                var counts = await _userGroupService.GetUsersCountPerGroupAsync();
                return Ok(counts);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the user counts per group. " + ex.Message);
            }
        }

    }
}
