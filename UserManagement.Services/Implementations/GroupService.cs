using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core.Models;
using UserManagement.Repository.Interfaces;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly IGenericRepository<Group> _groupRepository;

        public GroupService(IGenericRepository<Group> groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _groupRepository.GetAllAsync();
        }

        public async Task<Group> GetByIdAsync(object id)
        {
            return await _groupRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(Group entity)
        {
            await _groupRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(Group entity)
        {
            await _groupRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Group entity)
        {
            await _groupRepository.DeleteAsync(entity);
        }
    }
}
