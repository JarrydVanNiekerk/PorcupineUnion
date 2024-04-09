using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.Models
{
    public class User
    {
        // PK
        public int UserId { get; set; }
        public string Name { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    }
}
