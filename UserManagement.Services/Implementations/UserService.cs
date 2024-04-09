using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core.Models;
using UserManagement.Repository.Interfaces;
using UserManagement.Services.Interfaces;
using UserManagement.Core.Data;

namespace UserManagement.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly PorcupineDbContext _context;  

        public UserService(IGenericRepository<User> userRepository, PorcupineDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(object id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(User entity)
        {
            await _userRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(User entity)
        {
            await _userRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(User entity)
        {
            await _userRepository.DeleteAsync(entity);
        }

        public async Task<int> GetUserCountAsync()
        {
            return await _context.Set<User>().CountAsync();  
        }
    }
}
