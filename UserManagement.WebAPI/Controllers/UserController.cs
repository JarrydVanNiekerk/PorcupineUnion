using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Core.Models;
using UserManagement.Services.Interfaces;

namespace UserManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            foreach (var ug in user.UserGroups)
            {
                if (ug.Group == null && ug.GroupId > 0)
                {
                    ug.Group = new Group { GroupId = ug.GroupId }; 
                }
            }

            await _userService.InsertAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }


        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.UpdateAsync(user);
            return NoContent(); 
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteAsync(user);
            return NoContent();
        }
        [HttpGet("count")]
        public async Task<IActionResult> GetUserCount()
        {
            int count = await _userService.GetUserCountAsync();
            return Ok(count);
        }

    }
}
