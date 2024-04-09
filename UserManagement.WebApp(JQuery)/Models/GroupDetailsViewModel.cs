using UserManagement.Core.Models;

public class GroupDetailsViewModel
{
    public Group Group { get; set; }
    public List<Permission> AllPermissions { get; set; }
    public List<Permission> AssignedPermissions { get; set; }
}
