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
    public class GroupPermissionService : IGroupPermissionService
    {
        private readonly IGenericRepository<GroupPermission> _groupPermissionRepository;
        private readonly PorcupineDbContext _context;

        public GroupPermissionService(IGenericRepository<GroupPermission> groupPermissionRepository, PorcupineDbContext context)
        {
            _groupPermissionRepository = groupPermissionRepository;
            _context = context;
        }

        public async Task<IEnumerable<GroupPermission>> GetAllAsync()
        {
            return await _groupPermissionRepository.GetAllAsync();
        }

        public async Task<GroupPermission> GetByIdAsync(object id)
        {
            return await _groupPermissionRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(GroupPermission entity)
        {
            await _groupPermissionRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(GroupPermission entity)
        {
            await _groupPermissionRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(GroupPermission entity)
        {
            await _groupPermissionRepository.DeleteAsync(entity);
        }


    }
}
