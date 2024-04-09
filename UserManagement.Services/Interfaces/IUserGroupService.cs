using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core.Models;
using UserManagement.Repository.Interfaces;

namespace UserManagement.Services.Interfaces
{
    public interface IUserGroupService : IGenericRepository<UserGroup>
    {
        Task<Dictionary<int, int>> GetUsersCountPerGroupAsync();
    }
}
