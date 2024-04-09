using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Models;

namespace UserManagement.Core.Models
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public User? User { get; set; }

        public int GroupId { get; set; }
        public Group? Group { get; set; }
    }
}
