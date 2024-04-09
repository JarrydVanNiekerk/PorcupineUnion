using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core.Models;
using UserManagement.Repository.Interfaces;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Implementations
{
    public class PermissionService : IPermissionService
    {
        private readonly IGenericRepository<Permission> _permissionRepository;

        public PermissionService(IGenericRepository<Permission> permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _permissionRepository.GetAllAsync();
        }

        public async Task<Permission> GetByIdAsync(object id)
        {
            return await _permissionRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(Permission entity)
        {
            await _permissionRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(Permission entity)
        {
            await _permissionRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Permission entity)
        {
            await _permissionRepository.DeleteAsync(entity);
        }
    }
}
