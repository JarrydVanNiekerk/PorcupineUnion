using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Core.Models;
using UserManagement.Repository.Interfaces;
using UserManagement.Services.Interfaces;
using UserManagement.Core.Data;

namespace UserManagement.Services.Implementations
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IGenericRepository<UserGroup> _userGroupRepository;
        private readonly PorcupineDbContext _context;  

        public UserGroupService(IGenericRepository<UserGroup> userGroupRepository, PorcupineDbContext context)
        {
            _userGroupRepository = userGroupRepository;
            _context = context; 
        }

        public async Task<IEnumerable<UserGroup>> GetAllAsync()
        {
            return await _userGroupRepository.GetAllAsync();
        }

        public async Task<UserGroup> GetByIdAsync(object id)
        {
            return await _userGroupRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(UserGroup entity)
        {
            await _userGroupRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(UserGroup entity)
        {
            await _userGroupRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(UserGroup entity)
        {
            await _userGroupRepository.DeleteAsync(entity);
        }

        public async Task<Dictionary<int, int>> GetUsersCountPerGroupAsync()
        {
            return await _context.Set<UserGroup>()
                .GroupBy(ug => ug.GroupId)
                .Select(g => new { GroupId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.GroupId, g => g.Count);
        }
    }
}
